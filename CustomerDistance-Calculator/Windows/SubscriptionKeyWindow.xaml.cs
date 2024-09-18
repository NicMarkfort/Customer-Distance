using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomerDistance_Calculator.Windows
{
    /// <summary>
    /// Interaktionslogik für SubscriptionKeyWindow.xaml
    /// </summary>
    public partial class SubscriptionKeyWindow : Window
    {
        public string SubscriptionsKey { get => subscriptionKeyPb.Password; }

        public SubscriptionKeyWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SubscriptionsKey) || string.IsNullOrWhiteSpace(SubscriptionsKey))
            {
                MessageBox.Show("Bitte gebe einen Subscription-Key an!", "Subscription-Key");
                return;
            }
            DialogResult = true;
        }

    }
}
