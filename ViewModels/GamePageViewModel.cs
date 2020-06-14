using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        List<Games> games;

        public List<Games> Games
        {
            get
            {
                if (games == null)
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        games = new List<Games>();
                        games.AddRange(unitOfWork.Games.GetAll());
                    }
                }
                return games;
            }
        }

        #region commands
        
        #endregion
    }
}
