using System.Data.Linq.Mapping;
using System.ComponentModel;


namespace SmallInvestment.Dal.ModelEntity
{
    [Table]
    public class BankEntity : INotifyPropertyChanged,INotifyPropertyChanging
    {
        private int _bankID;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BankID {
            get { return _bankID; }
            set 
            {
                if (_bankID != value)
                {
                    NotifyPropertyChanging("BankID");
                    _bankID = value;
                    NotifyPropertyChanged("BankID");
                }
            }
        }


        private string _bankName;

        [Column]
        public string BankName
        {
            get { return _bankName; }
            set 
            {
                if (_bankName != value)
                {
                    NotifyPropertyChanging("BankName");
                    _bankName = value;
                    NotifyPropertyChanged("BankName");
                }
            }
        }



        private string _bankGroup;

        [Column]
        public string BankGroup
        {
            get { return _bankGroup; }
            set
            {
                if (_bankGroup != value)
                {
                    NotifyPropertyChanging("BankGroup");
                    _bankGroup = value;
                    NotifyPropertyChanged("BankGroup");
                }
            }
        }

        #region 活期存款利率
        private int _currentRadio;

        [Column]
        public int CurrentRadio
        {
            get { return _currentRadio; }
            set
            {
                if (_currentRadio != value)
                {
                    NotifyPropertyChanging("CurrentRadio");
                    _currentRadio = value;
                    NotifyPropertyChanged("CurrentRadio");
                }
            }
        }
        #endregion

        #region 整存整取利率

        private int _radio3m;

        [Column]
        public int Radio3m
        {
            get { return _radio3m; }
            set {
                if (_radio3m != value)
                {
                    NotifyPropertyChanging("Radio3m");
                    _radio3m = value;
                    NotifyPropertyChanged("Radio3m");
                }
            }
        }

        private int _radio6m;

        [Column]
        public int Radio6m 
        {
            get { return _radio6m; }
            set 
            {
                if (_radio6m != value)
                {
                    NotifyPropertyChanging("Radio6m");
                    _radio6m = value;
                    NotifyPropertyChanged("Radio6m");
                }
            }
        }


        private int _radio1y;

        [Column]
        public int Radio1y
        {
            get { return _radio1y; }
            set 
            {
                if (_radio1y != value)
                {
                    NotifyPropertyChanging("Radio1y");
                    _radio1y = value;
                    NotifyPropertyChanged("Radio1y");
                }
            }
        }


        private int _radio2y;

        [Column]
        public int Radio2y
        {
            get { return _radio2y; }
            set
            {
                if (_radio2y != value)
                {
                    NotifyPropertyChanging("Radio2y");
                    _radio2y = value;
                    NotifyPropertyChanged("Radio2y");
                }
            }
        }


        private int _radio3y;

        [Column]
        public int Radio3y
        {
            get { return _radio3y; }
            set 
            {
                if (_radio3y != value)
                {
                    NotifyPropertyChanging("Radio3y");
                    _radio3y = value;
                    NotifyPropertyChanged("Radio3y");
                }
            }
        }


        private int _radio5y { get; set; }

        [Column]
        public int Radio5y
        {
            get { return _radio5y; }
            set 
            {
                if (_radio5y != value)
                {
                    NotifyPropertyChanging("Radio5y");
                    _radio5y = value;
                    NotifyPropertyChanged("Radio5y");
                }
            }
        }
        #endregion

        #region 零存整取、整存零取、存本取息 利率
        private int _disperse_Radio1y;

        [Column]
        public int Disperse_Radio1y 
        {
            get { return _disperse_Radio1y; }
            set
            {
                if (_disperse_Radio1y != value)
                {
                    NotifyPropertyChanging("Disperse_Radio1y");
                    _disperse_Radio1y = value;
                    NotifyPropertyChanged("Disperse_Radio1y");
                }
            }
        }

        private int _disperse_Radio3y;

        [Column]
        public int Disperse_Radio3y
        {
            get { return _disperse_Radio3y; }
            set
            {
                if (_disperse_Radio3y != value)
                {
                    NotifyPropertyChanging("Disperse_Radio3y");
                    _disperse_Radio3y = value;
                    NotifyPropertyChanged("Disperse_Radio3y");
                }
            }
        }


        private int _disperse_Radio5y;

        [Column]
        public int Disperse_Radio5y
        {
            get { return _disperse_Radio5y; }
            set
            {
                if (_disperse_Radio5y != value)
                {
                    NotifyPropertyChanging("Disperse_Radio5y");
                    _disperse_Radio5y = value;
                    NotifyPropertyChanged("Disperse_Radio5y");
                }
            }
        }

        private string _metroColor;

        [Column]
        public string MetroColor
        {
            get { return _metroColor; }
            set
            {
                if (_metroColor != value)
                {
                    NotifyPropertyChanging("MetroColor");
                    _metroColor = value;
                    NotifyPropertyChanged("MetroColor");
                }
            }
        }

        public int _updateTime;

        [Column]
        public int UpdateTime
        {
            get { return _updateTime; }
            set
            {
                if (_updateTime != value)
                {
                    NotifyPropertyChanging("UpdateTime");
                    _updateTime = value;
                    NotifyPropertyChanged("UpdateTime");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

    }
}
