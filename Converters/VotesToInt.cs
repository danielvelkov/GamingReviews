using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GamingReviews.Converters
{
    public class VotesToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int votesCount = 0;
            if(value is ObservableCollection<Votes>)
            {
                var votes = value as ObservableCollection<Votes>;
                foreach(var vote in votes)
                {
                    if (vote.Reaction == Reaction.Liked)
                        votesCount++;
                    else votesCount--;
                }
            }
            return votesCount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
