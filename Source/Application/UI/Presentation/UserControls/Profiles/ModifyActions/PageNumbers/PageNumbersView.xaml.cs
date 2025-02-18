﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using pdfforge.PDFCreator.UI.Presentation.Helper;
using pdfforge.PDFCreator.UI.Presentation.UserControls.Profiles.ModifyActions.Background;
using Prism.Regions;

namespace pdfforge.PDFCreator.UI.Presentation.UserControls.Profiles.ModifyActions.PageNumbers
{
    /// <summary>
    /// Interaction logic for PageNumbersView.xaml
    /// </summary>
    public partial class PageNumbersView : UserControl, IRegionMemberLifetime, IActionUserControl
    {
        private static readonly Regex NumberRegex = new Regex(@"-?[0-9]*(\.?[0-9]*)?");

        public bool KeepAlive { get; } = true;

        public PageNumbersView(PageNumbersViewModel viewModel)
        {
            DataContext = viewModel;
            ViewModel = viewModel;
            TransposerHelper.Register(this, viewModel);
            InitializeComponent();
        }

        public IActionViewModel ViewModel { get; }

        private static bool IsNumber(string text)
        {
            return NumberRegex.IsMatch(text);
        }

        private void OnTextEnteredToNumberField(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumber(e.Text);
        }
        private void NumberBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsNumber(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
