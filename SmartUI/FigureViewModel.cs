namespace SmartUI
{
    public class FigureViewModel : VmBase
    {
        public MFCItem MFC { get; set; }

        public bool Custom;
        private int _mfcId;
        private string _img;
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Image
        {
            get => _img;
            set => SetProperty(ref _img, value);
        }
        public int MfcId
        {
            get => _mfcId;
            set => SetProperty(ref _mfcId, value);
        }
        private decimal _latestAmazon;
        public decimal LatestAmazon
        {
            get => _latestAmazon;
            set => SetProperty(ref _latestAmazon, value);
        }

        public FigureViewModel()
        {
            //_img = @"C:\Users\BLaze\Pictures\9be2ox37b9d91.webp";
            _img = @"C:\Users\BLaze\Pictures\05e4a1990d3330893dda838588e557b9_1286220646533536129.jpg";
            _name = "Albedo";
        }

        public FigureViewModel(MFCItem m)
        {
            _mfcId = m.MFCId;
            _name = m.Title;
            _img = Glob.FullPic(_mfcId);
            MFC = m;
        }
    }
}
