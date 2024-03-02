using System;
using System.Windows.Input;

namespace SmartUI
{
    public class InfoViewModel : VmBase
    {
        #region Constructors
        public bool open { get; set; }
        public FigureViewModel Fig { get; set; }
        private double _FREEing;
        public double FREEing
        {
            get => _FREEing;
            set => SetProperty(ref _FREEing, value);
        }

        public InfoViewModel(FigureViewModel f)
        {
            Fig = f;
            ;
            //OnRequestClose(this, new EventArgs());
        }

        public event EventHandler RequestClose;

        public void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region Private fields

        //private Net net = new();
        //private db Db = new();

        #endregion

        #region Public Properties

        public ICommand X
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Console.WriteLine("X");
                    OnRequestClose();
                });
            }
        }

        #endregion
    }
}
