using Economic_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Economic_Game.DAL;
using System.Data.Entity;

namespace Economic_Game.Controllers
{
    public class HomeController : Controller
    {
        public EconomicGameDataContext db = new EconomicGameDataContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            ViewBag.Title = "Home";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            GameSettings currentSettings = db.GameSettings.FirstOrDefault(x => x.SelectedSettings == true);

            Game game = new Game();
            game.AmazonID = model.AmazonID;
            game.Round1Multiplier = currentSettings.Round1Multiplier;
            game.Round2Multiplier = currentSettings.Round2Multiplier;
            game.Round3Multiplier = currentSettings.Round3Multiplier;
            game.Round4Multiplier = currentSettings.Round4Multiplier;

            game.Round1ReturnPercentage = currentSettings.Round1ReturnPercentage;
            game.Round2ReturnPercentage = currentSettings.Round2ReturnPercentage;
            game.Round3ReturnPercentage = currentSettings.Round3ReturnPercentage;
            game.Round4ReturnPercentage = currentSettings.Round4ReturnPercentage;

            game.StartingAmount = currentSettings.StartingAmount;
            game.ComputerApology = currentSettings.ComputerApology;
            game.ComputerPersuasion = currentSettings.ComputerPersuasion;

            game.GameSettingsID = currentSettings.GameSettingsID;
            game.PlayerTotal = currentSettings.StartingAmount;
            
            db.Games.Add(game);
            db.SaveChanges();

            ViewData["Questions"] = db.Questions.ToList();

            return RedirectToAction("Game", new { id = game.GameID });
        }

        public ActionResult Game(int? id)
        {
            Game game = db.Games.Find(id);
            RoundViewModel model = new RoundViewModel();


            switch (game.CurrentRound)
            {
                case 1:
                    ViewBag.Title = "Round 1";
                    model.GameID = game.GameID;
                    model.CurrentRound = 1;
                    model.CurrentEarnings = 0;
                    model.RoundType = "Practice Round";
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round1Multiplier;
                    model.MultiplierText = getNumberString(game.Round1Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    model.ReturnPercentage = game.Round1ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;
                    model.Message1 = "This round is just practice, right?";
                    model.Message2 = "Oh yeah, you can't talk to me.";
                    model.Message3 = "Sorry";
                    ViewData["Questions"] = db.Questions.ToList();
                    return View(model);
                case 2:
                    model.GameID = game.GameID;
                    model.CurrentRound = 2;
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round2Multiplier;
                    model.MultiplierText = getNumberString(game.Round2Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    model.ReturnPercentage = game.Round2ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;

                    return View(model);
            }
            return RedirectToAction("Summary", new { id = game.GameID });
        }

        [HttpPost]
        public ActionResult Game(RoundViewModel model)
        {
            Game game = db.Games.Find(model.GameID);

            double roundInvestment = 0.00;
            string rawRoundInvestment = model.RoundInvestmentDollars.ToString() + '.' + model.RoundInvestmentCents.ToString();
            if (Double.TryParse(rawRoundInvestment, out roundInvestment))
            {
                roundInvestment = double.Parse(rawRoundInvestment);
            }

            double roundKept = 0.00;
            string rawRoundKept = model.RoundKeptDollars.ToString() + '.' + model.RoundKeptCents.ToString();
            if (Double.TryParse(rawRoundKept, out roundKept))
            {
                roundKept = double.Parse(rawRoundKept);
            }


            if (roundInvestment == 0 && roundKept == 0)
            {
                ViewBag.Error = "You must invest money or indicate you wish to keep money.";
                return View(model);
            }
            else if (roundInvestment > 0 && roundKept > 0)
            {
                if (((decimal)roundInvestment + (decimal)roundKept) > model.StartingAmount || ((decimal)roundInvestment + (decimal)roundKept) < model.StartingAmount)
                {
                    ViewBag.Error = "The total invested and the total kept combined must equal " + model.StartingAmount.ToString();
                    return View(model);
                }
            }
            else if (roundInvestment == 0 && ((decimal)roundKept < model.StartingAmount || (decimal)roundKept > model.StartingAmount))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal " + model.StartingAmount.ToString();
                return View(model);
            }
            else if (roundKept == 0 && ((decimal)roundInvestment < model.StartingAmount || (decimal)roundInvestment > model.StartingAmount))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal " + model.StartingAmount.ToString();
                return View(model);
            }

            ModelState.Clear();

            ViewData["Questions"] = db.Questions.ToList();

            switch (model.CurrentRound)
            {
                case 1:

                    game.CurrentRound = 2;

                    if(roundKept > 0)
                    {
                        game.Round1Kept = (decimal)roundKept;
                        game.PlayerTotal += (decimal)roundKept;
                    }
                    else
                    {
                        game.Round1Kept = 0;
                    }

                    if (roundInvestment > 0)
                    {
                        game.Round1Investment = (decimal)roundInvestment;
                        game.Round1Returned = (model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage;
                        //game.PlayerTotal += (model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage;
                        game.PlayerTotal = 0;
                    }
                    else
                    {
                        game.Round1Investment = 0;
                    }

                    db.Entry(game).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Title = "Round 2";
                    model.GameID = game.GameID;
                    model.CurrentRound = 2;
                    model.RoundType = "Real Money";
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round2Multiplier;
                    model.MultiplierText = getNumberString(game.Round2Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    model.ReturnPercentage = game.Round2ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;
                    model.CurrentEarnings = 0;
                    model.Message1 = "do you want to work together?";
                    model.Message2 = "Either way";
                    model.Message3 = "Sorry";
                    ViewBag.GameMessage = "Seller has decided to return " + game.Round1Returned.Value.ToString("C");
                    return View(model);
                case 2:
                    game.CurrentRound = 3;

                    if (roundKept > 0)
                    {
                        game.Round2Kept = (decimal)roundKept;
                        game.PlayerTotal += (decimal)roundKept;
                    }
                    else
                    {
                        game.Round2Kept = 0;
                    }

                    if (roundInvestment > 0)
                    {
                        game.Round2Investment = (decimal)roundInvestment;
                        game.Round2Returned = (model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage;
                        game.PlayerTotal += ((model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage);
                    }
                    else
                    {
                        game.Round2Investment = 0;
                    }

                    db.Entry(game).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Title = "Round 3";
                    model.GameID = game.GameID;
                    model.CurrentRound = 3;
                    model.RoundType = "Real Money";
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round3Multiplier;
                    model.MultiplierText = getNumberString(game.Round3Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    model.ReturnPercentage = game.Round3ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;
                    ViewBag.GameMessage = "Seller has decided to return " + game.Round2Returned.Value.ToString("C");
                    model.CurrentEarnings = game.PlayerTotal;
                    model.Message1 = "";
                    model.Message2 = "Hmmm";
                    model.Message3 = "";
                    return View(model);
                case 3:
                    game.CurrentRound = 4;

                    if (roundKept > 0)
                    {
                        game.Round3Kept = (decimal)roundKept;
                        game.PlayerTotal += (decimal)roundKept;
                    }
                    else
                    {
                        game.Round3Kept = 0;
                    }

                    if (roundInvestment > 0)
                    {
                        game.Round3Investment = (decimal)roundInvestment;
                        game.Round3Returned = (model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage;
                        game.PlayerTotal += ((model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage);
                    }
                    else
                    {
                        game.Round3Investment = 0;
                    }

                    db.Entry(game).State = EntityState.Modified;
                    db.SaveChanges();

                    model.GameID = game.GameID;
                    model.CurrentRound = 4;
                    model.RoundType = "Real Money";
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round4Multiplier;
                    model.MultiplierText = getNumberString(game.Round4Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    model.CurrentEarnings = game.PlayerTotal;
                    model.ReturnPercentage = game.Round4ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;
                    ViewBag.GameMessage = "Seller has decided not to return any of your investment.";
                    model.Message1 = "So that was a mistake.";
                    model.Message2 = "";
                    model.Message3 = "I won't do that again.";
                    return View(model);
                case 4:
                    game.CurrentRound = 4;

                    if (roundKept > 0)
                    {
                        game.Round4Kept = (decimal)roundKept;
                        game.PlayerTotal += (decimal)roundKept;
                    }
                    else
                    {
                        game.Round4Kept = 0;
                    }

                    if (roundInvestment > 0)
                    {
                        game.Round4Investment = (decimal)roundInvestment;
                        game.Round4Returned = (model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage;
                        game.PlayerTotal += ((model.Multiplier * (decimal)roundInvestment) * model.ReturnPercentage);
                    }
                    else
                    {
                        game.Round4Investment = 0;
                    }

                    db.Entry(game).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Title = "Round 4";
                    model.GameID = game.GameID;
                    model.CurrentRound = 4;
                    model.RoundType = "Real Money";
                    model.ComputerPersuasion = game.ComputerPersuasion;
                    model.ComputerApology = game.ComputerApology;
                    model.Multiplier = game.Round4Multiplier;
                    model.MultiplierText = getNumberString(game.Round4Multiplier);
                    model.PlayerTotal = game.PlayerTotal;
                    ViewBag.GameMessage = "Seller has decided to return " + game.Round4Returned.Value.ToString("C");
                    model.CurrentEarnings = game.PlayerTotal;
                    model.ReturnPercentage = game.Round4ReturnPercentage;
                    model.StartingAmount = game.StartingAmount;
                    //return RedirectToAction("Summary", new { id = model.GameID });
                    ViewBag.EndGame = true;
                    return View(model);
            }
            return RedirectToAction("Summary", new { id = game.GameID });
        }

        public ActionResult Round1(int? id)
        {
            Game game = db.Games.Find(id);
            RoundViewModel model = new RoundViewModel();

            model.GameID = game.GameID;
            model.CurrentRound = 1;
            model.ComputerPersuasion = game.ComputerPersuasion;
            model.Multiplier = game.Round1Multiplier;
            model.PlayerTotal = game.PlayerTotal;
            model.ReturnPercentage = game.Round1ReturnPercentage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Round1(RoundViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(game);
            //}

            if (model.RoundInvestment == null && model.RoundKept == null)
            {
                ViewBag.Error = "You must invest money or indicate you wish to keep money.";
                return View(model);
            }
            else if (model.RoundKept != null && model.RoundInvestment != null)
            {
                if (((decimal)model.RoundInvestment + (decimal)model.RoundKept) > 1 || ((decimal)model.RoundInvestment + (decimal)model.RoundKept) < 1)
                {
                    ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                    return View(model);
                }
            }
            else if (model.RoundInvestment == null && (model.RoundKept < 1 || model.RoundKept > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }
            else if (model.RoundKept == null && (model.RoundInvestment < 1 || model.RoundInvestment > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }

            model.CurrentRound = 2;
            
            model.RoundReturned = (model.Multiplier * model.RoundInvestment) * model.ReturnPercentage;
            if(model.RoundKept == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if(model.RoundInvestment == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if(model.RoundKept != null && model.RoundReturned != null)
            {
                model.PlayerTotal = model.RoundKept + model.RoundReturned;
            }
            //game.PlayerTotal = game.Round1Kept + game.Round1Returned;

            Game game = db.Games.Find(model.GameID);
            
            if(game != null)
            {
                game.Round1Investment = model.RoundInvestment;
                game.Round1Kept = model.RoundKept;
                game.Round1Returned = model.RoundReturned;
                game.PlayerTotal = model.PlayerTotal;

                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return RedirectToAction("Round2", new { id = game.GameID });
        }

        public ActionResult Round2(int? id)
        {
            Game game = db.Games.Find(id);
            RoundViewModel model = new RoundViewModel();
            model.GameID = game.GameID;

            ViewBag.GameMessage = "Seller has decided to return " + game.Round1Returned.Value.ToString("C");
            model.CurrentRound = 2;
            model.StartingAmount = game.StartingAmount;
            model.ComputerPersuasion = game.ComputerPersuasion;
            model.Multiplier = game.Round2Multiplier;
            model.PlayerTotal = game.PlayerTotal;
            model.CurrentEarnings = game.PlayerTotal;
            model.ReturnPercentage = game.Round2ReturnPercentage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Round2(RoundViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(game);
            //}

            if (model.RoundInvestment == null && model.RoundKept == null)
            {
                ViewBag.Error = "You must invest money or indicate you wish to keep money.";
                return View(model);
            }
            else if (model.RoundKept != null && model.RoundInvestment != null)
            {
                if (((decimal)model.RoundInvestment + (decimal)model.RoundKept) > 1 || ((decimal)model.RoundInvestment + (decimal)model.RoundKept) < 1)
                {
                    ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                    return View(model);
                }
            }
            else if (model.RoundInvestment == null && (model.RoundKept < 1 || model.RoundKept > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }
            else if (model.RoundKept == null && (model.RoundInvestment < 1 || model.RoundInvestment > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }

            model.CurrentRound = 2;

            model.RoundReturned = (model.Multiplier * model.RoundInvestment) * model.ReturnPercentage;

            if (model.RoundKept == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundInvestment == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundKept != null && model.RoundReturned != null)
            {
                model.PlayerTotal = model.RoundKept + model.RoundReturned;
            }
            //game.PlayerTotal = game.Round1Kept + game.Round1Returned;

            Game game = db.Games.Find(model.GameID);

            if (game != null)
            {
                game.Round2Investment = model.RoundInvestment;
                game.Round2Kept = model.RoundKept;
                game.Round2Returned = model.RoundReturned;
                game.PlayerTotal = model.PlayerTotal;
                ViewBag.GameMessage = "Seller has decided to return " + game.Round2Returned;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Round3", new { id = game.GameID });
        }

        public ActionResult Round3(int? id)
        {
            Game game = db.Games.Find(id);
            RoundViewModel model = new RoundViewModel();
            model.GameID = game.GameID;

            model.CurrentRound = 3;
            model.ComputerPersuasion = game.ComputerPersuasion;
            model.ComputerApology = game.ComputerApology;
            model.Multiplier = game.Round3Multiplier;
            model.ReturnPercentage = game.Round2ReturnPercentage;
            model.PlayerTotal = game.PlayerTotal;
            return View(model);
        }

        [HttpPost]
        public ActionResult Round3(RoundViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(game);
            //}

            if (model.RoundInvestment == null && model.RoundKept == null)
            {
                ViewBag.Error = "You must invest money or indicate you wish to keep money.";
                return View(model);
            }
            else if (model.RoundKept != null && model.RoundInvestment != null)
            {
                if (((decimal)model.RoundInvestment + (decimal)model.RoundKept) > 1 || ((decimal)model.RoundInvestment + (decimal)model.RoundKept) < 1)
                {
                    ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                    return View(model);
                }
            }
            else if (model.RoundInvestment == null && (model.RoundKept < 1 || model.RoundKept > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }
            else if (model.RoundKept == null && (model.RoundInvestment < 1 || model.RoundInvestment > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }

            model.CurrentRound = 3;

            model.RoundReturned = (model.Multiplier * model.RoundInvestment) * model.ReturnPercentage;
            if (model.RoundKept == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundInvestment == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundKept != null && model.RoundReturned != null)
            {
                model.PlayerTotal = model.RoundKept + model.RoundReturned;
            }
            //game.PlayerTotal = game.Round1Kept + game.Round1Returned;

            Game game = db.Games.Find(model.GameID);

            if (game != null)
            {
                game.Round3Investment = model.RoundInvestment;
                game.Round3Kept = model.RoundKept;
                game.Round3Returned = model.RoundReturned;
                game.PlayerTotal = model.PlayerTotal;
                ViewBag.GameMessage = "Seller has decided to return " + game.Round3Returned;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Round4", new { id = game.GameID });
        }

        public ActionResult Round4(int? id)
        {
            Game game = db.Games.Find(id);
            RoundViewModel model = new RoundViewModel();

            model.GameID = game.GameID;
            model.CurrentRound = 4;
            model.ComputerPersuasion = game.ComputerPersuasion;
            model.Multiplier = game.Round4Multiplier;
            model.PlayerTotal = game.PlayerTotal;
            return View(model);
        }

        [HttpPost]
        public ActionResult Round4(RoundViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(game);
            //}

            if (model.RoundInvestment == null && model.RoundKept == null)
            {
                ViewBag.Error = "You must invest money or indicate you wish to keep money.";
                return View(model);
            }
            else if (model.RoundKept != null && model.RoundInvestment != null)
            {
                if (((decimal)model.RoundInvestment + (decimal)model.RoundKept) > 1 || ((decimal)model.RoundInvestment + (decimal)model.RoundKept) < 1)
                {
                    ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                    return View(model);
                }
            }
            else if (model.RoundInvestment == null && (model.RoundKept < 1 || model.RoundKept > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }
            else if (model.RoundKept == null && (model.RoundInvestment < 1 || model.RoundInvestment > 1))
            {
                ViewBag.Error = "The total invested and the total kept combined must equal $1.00";
                return View(model);
            }

            model.CurrentRound = 4;

            model.RoundReturned = model.Multiplier * model.RoundInvestment;
            if (model.RoundKept == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundInvestment == null)
            {
                model.PlayerTotal = model.RoundReturned;
            }
            else if (model.RoundKept != null && model.RoundReturned != null)
            {
                model.PlayerTotal = model.RoundKept + model.RoundReturned;
            }
            //game.PlayerTotal = game.Round1Kept + game.Round1Returned;

            Game game = db.Games.Find(model.GameID);

            if (game != null)
            {
                game.Round4Investment = model.RoundInvestment;
                game.Round4Kept = model.RoundKept;
                game.Round4Returned = model.RoundReturned;
                game.PlayerTotal = model.PlayerTotal;

                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("End", new { id = game.GameID });
        }

        public ActionResult Summary(int id)
        {
            Game game = db.Games.Find(id);

            ViewBag.Title = "Summary";
            if(game != null)
            {
                return View(game);
            }
            return View();
        }

        public string getNumberString(int? num)
        {
            switch (num)
            {
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                default:
                    return null;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}