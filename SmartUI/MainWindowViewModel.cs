using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SmartUI
{
    public class MainWindowViewModel : VmBase
    {
        #region Constructors
        public MainWindowViewModel()
        {
            Load();
            CollectionView = CollectionViewSource.GetDefaultView(_collectionViewModels);
        }
        #endregion

        private void Load()
        {
            foreach (var item in Db.Load())
            {
                _collectionViewModels.Add(new FigureViewModel(item));
            }
        }

        #region Private fields

        private Net net = new();
        private readonly List<FigureViewModel> _collectionViewModels = new();
        private FigureViewModel _selectedFigure;
        private db Db = new();

        #endregion

        #region Public properties
        public void Closing()
        {
            List<FigureViewModel> vv = new();

            if (InfoDialogPtr != null)
            {
                InfoDialogPtr.Close();
            }
        }
        public ICollectionView CollectionView { get; init; }

        public ICommand MoreImagesCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    Console.WriteLine(_selectedFigure.Name);
                    //return;
                    await net.Image(_selectedFigure.MfcId);
                    HtmlParse html = new();
                    var itm = await html.Pars($"https://myfigurecollection.net/item/{_selectedFigure.MfcId}");

                    //var it = await html.GetItem(itm);
                    var dir = Glob.MorePic + _selectedFigure.MfcId + "\\";
                    if (Directory.Exists(dir) == false)
                    {
                        Directory.CreateDirectory(dir);
                        var ls = await html.ImgSlides(itm);
                        for (int i = 0; i < ls.Count; i++)
                        {
                            string ola = ".jpeg";
                            if (ls[i].Contains(ola) == false)
                            {
                                ola = ".png";
                            }
                            if (ls[i].Contains(ola) == false)
                            {
                                ola = ".jpg";
                            }
                            var fileToWrite = dir + (i + 1) + ola;
                            await net.HttpGetForLargeFileInRightWay(ls[i], fileToWrite);
                        }
                    }
                });
            }
        }
        public ICommand OpenMoreImagesFolderCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var dir = Glob.MorePic + _selectedFigure.MfcId + "\\";
                    if (Directory.Exists(dir))
                    {
                        Process.Start("explorer.exe", dir);
                    }
                    else
                    {
                        Directory.CreateDirectory(dir);
                    }
                });
            }
        }
        public ICommand OpenImagesFolderCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Clipboard.SetText(SelectedFigure.MfcId.ToString());
                    var dir = Glob.Pic; //+ _selectedFigure.MfcId + "\\";
                    //if (Directory.Exists(dir))
                    {
                        Process.Start("explorer.exe", dir);
                    }
                });
            }
        }
        public ICommand FromMFCIdCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    bool success = int.TryParse(Clipboard.GetText(), out int id);
                    if (!success) { Console.WriteLine("enter a valid MFC id integer"); return; }
                    await net.Image(id);
                    HtmlParse html = new();
                    var itm = await html.Pars($"https://myfigurecollection.net/item/{id}");

                    var it = await html.GetItem(itm);
                    var dir = Glob.MorePic + id + "\\";
                    if (Directory.Exists(dir) == false)
                    {
                        Directory.CreateDirectory(dir);
                        var ls = await html.ImgSlides(itm);
                        for (int i = 0; i < ls.Count; i++)
                        {
                            string ext = ".jpeg";
                            if (ls[i].Contains(ext) == false)
                            {
                                ext = ".png";
                            }
                            if (ls[i].Contains(ext) == false)
                            {
                                ext = ".jpg";
                            }
                            var fileToWrite = dir + (i + 1) + ext;
                            await net.HttpGetForLargeFileInRightWay(ls[i], fileToWrite);
                        }
                    }
                    Db.i(it);
                    _collectionViewModels.Add(new FigureViewModel(it));
                    CollectionView.Refresh();
                });
            }
        }
        private InfoDialog InfoDialogPtr;
        public ICommand DoubleCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (InfoDialogPtr != null)
                    {
                        InfoDialogPtr.Close();
                    }
                    InfoViewModel fm = new(SelectedFigure);
                    InfoDialogPtr = new(fm);
                    //InfoDialogPtr.DataContext = fm;
                    InfoDialogPtr.Show();
                });
            }
        }

        public ICommand X
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Console.WriteLine("X");
                    InfoDialogPtr.Close();
                });
            }
        }

        public void ChangedSelection()
        {
            {
                InfoViewModel fm = new(SelectedFigure);
                InfoDialogPtr.DataContext = fm;
            }
        }

        public FigureViewModel SelectedFigure
        {
            get => _selectedFigure;
            set
            {
                _selectedFigure = value;
                RaisePropertyChanged(nameof(SelectedFigure));
                //if (oh) ChangedSelection();
                 ChangedSelection();
            }
        }

        #endregion
    }
}
