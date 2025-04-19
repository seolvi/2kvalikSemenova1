using _2kvalikSemenova.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _2kvalikSemenova
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static kvalik2SemenovaEntities db = new kvalik2SemenovaEntities();

        public static User loggedUser;
        public static Frame MainFrame; 
    }
}
