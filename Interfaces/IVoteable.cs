using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Interfaces
{
    public interface IVoteable
    {
        ObservableCollection<Votes> Votes { get; set; }
    }
}
