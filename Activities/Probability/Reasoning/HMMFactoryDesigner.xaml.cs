using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CosmicFlow.AIMA.Core.Util.DataStructure;

namespace CosmicFlow.AIMA.Probability.Reasoning
{
    // Interaction logic for HMMFactoryDesigner.xaml
    public partial class HMMFactoryDesigner
    {
        public HMMFactoryDesigner()
        {
            InitializeComponent();
        }

        private void AddStateButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = FindTextBoxSibling(sender, "StateTextBox");
            ModelItem.Properties["States"].Collection.Add(textBox.Text);
            textBox.Clear();
        }

        private void AddPerceptionButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = FindTextBoxSibling(sender,"PerceptionTextBox");
            ModelItem.Properties["Perceptions"].Collection.Add(textBox.Text);
            textBox.Clear();
        }

        private void AddTransitionProbabilityButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox startStateTextBox = FindTextBoxSibling(sender, "StartStateTextBox");
            TextBox endStateTextBox = FindTextBoxSibling(sender, "EndStateTextBox");
            TextBox probabilityTextBox = FindTextBoxSibling(sender, "ProbabilityTextBox");
            Triplet<String, String, Double> tuple = new Triplet<string, string, double>(startStateTextBox.Text, endStateTextBox.Text, Double.Parse(probabilityTextBox.Text));

            ModelItem.Properties["TransitionProbabilities"].Collection.Add(tuple);
            startStateTextBox.Clear();
            endStateTextBox.Clear();
            probabilityTextBox.Clear();

        }

        private void AddSensingProbabilityButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox stateTextBox = FindTextBoxSibling(sender, "SensedStateTextBox");
            TextBox sensedPerceptionTextBox = FindTextBoxSibling(sender, "SensedPerceptionTextBox");
            TextBox probabilityTextBox = FindTextBoxSibling(sender, "SensingProbabilityTextBox");
            Triplet<String, String, Double> tuple = new Triplet<string, string, double>(stateTextBox.Text, sensedPerceptionTextBox.Text, Double.Parse(probabilityTextBox.Text));

            ModelItem.Properties["SensingProbabilities"].Collection.Add(tuple);
            stateTextBox.Clear();
            sensedPerceptionTextBox.Clear();
            probabilityTextBox.Clear();
        }
        
        private TextBox FindTextBoxSibling(object sender, string name)
        {
            return ((StackPanel)((Button)sender).Parent).FindName(name) as TextBox;
        }
    }
}
