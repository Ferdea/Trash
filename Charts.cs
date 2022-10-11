using System;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using ZedGraph;
using Label = System.Windows.Forms.Label;

namespace Names
{
    class Charts
    {
        private Form form;
        private Button leftButton;
        private Button rightButton;
        private HashSet<string> namesSet;
        private NameData[] names;
        private Label label;
        private Label notification;
        
        public Charts(NameData[] names)
        {
            form = new Form
            {
                Text = "Ерунда",
                Size = new Size(800, 600)
            };

            leftButton = new Button
            {
                Size = new Size(200, 80),
                Left = 150,
                Top = 450
            };
            form.Controls.Add(leftButton);

            rightButton = new Button
            {
                Size = new Size(200, 80),
                Left = 400,
                Top = 450
            };
            form.Controls.Add(rightButton);

            this.names = names;
            namesSet = new HashSet<string>();
            foreach (var item in names)
            {
                if (!namesSet.Contains(item.Name))
                    namesSet.Add(item.Name);
            }

            label = new Label
            {
                Size = new Size(500, 100),
                Location = new Point(250, 150)
            };
            form.Controls.Add(label); 
            
            notification = new Label
            {
                Size = new Size(200, 100),
                Location = new Point(350, 250)
            };
            form.Controls.Add(notification);  
        }

        public void Start() 
        {
            var random = new Random();
            var firstName = namesSet.ToArray()[random.Next(namesSet.Count)];
            var firstBirthCount = Task.GetData(names, firstName);
            leftButton.Text = firstName;
            
            var secondName = namesSet.ToArray()[random.Next(namesSet.Count)];
            var secondBirthCount = Task.GetData(names, secondName);
            rightButton.Text = secondName;

            label.Text = String.Format("Какое имя встречается чаще? {0} или {1}", firstName, secondName);
            notification.Text = "";

            void OnLeftButtonOnClick(object sender, EventArgs args)
            {
                if (firstBirthCount < secondBirthCount)
                {
                    notification.Text =
                        String.Format("Неправильно! Людей с именем {0} родилось {1}, с именем {2} - {3}", firstName,
                            firstBirthCount, secondName, secondBirthCount);
                    Console.Beep();
                }
                else
                    notification.Text =
                        String.Format("Правильно! Людей с именем {0} родилось {1}, с именем {2} - {3}", firstName,
                            firstBirthCount, secondName, secondBirthCount);
                secondName = namesSet.ToArray()[random.Next(namesSet.Count)];
                secondBirthCount = Task.GetData(names, secondName);
                rightButton.Text = secondName;
                label.Text = String.Format("Какое имя встречается чаще? {0} или {1}", firstName, secondName);
            }

            leftButton.Click += OnLeftButtonOnClick;
            
            void OnRightButtonOnClick(object sender, EventArgs args)
            {
                if (firstBirthCount > secondBirthCount)
                {
                    notification.Text =
                        String.Format("Неправильно! Людей с именем {0} родилось {1}, с именем {2} - {3}", firstName,
                            firstBirthCount, secondName, secondBirthCount);
                    Console.Beep();
                }
                else
                    notification.Text =
                        String.Format("Правильно! Людей с именем {0} родилось {1}, с именем {2} - {3}", firstName,
                            firstBirthCount, secondName, secondBirthCount);
                firstName = namesSet.ToArray()[random.Next(namesSet.Count)];
                firstBirthCount = Task.GetData(names, firstName);
                leftButton.Text = firstName;
                label.Text = String.Format("Какое имя встречается чаще? {0} или {1}", firstName, secondName);
            }
            rightButton.Click += OnRightButtonOnClick;
            
            form.ShowDialog();
        }
    }
}