using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common.Models;

namespace Vaultify.Droid.Common
{
    public class Constants
    {
        public static List<CardModel> getEmployeeData()
        {
            // create an ArrayList of type Employee class
            List<CardModel> employeeList
                = new List<CardModel>();
            CardModel emp1 = new CardModel("Chinmaya Mohapatra",
                                         "chinmaya@gmail.com");
            employeeList.Add(emp1);
            CardModel emp2
                = new CardModel("Ram prakash", "ramp@gmail.com");
            employeeList.Add(emp2);
            CardModel emp3 = new CardModel("OMM Meheta",
                                         "mehetaom@gmail.com");
            employeeList.Add(emp3);
            CardModel emp4 = new CardModel("Hari Mohapatra",
                                         "harim@gmail.com");
            employeeList.Add(emp4);
            CardModel emp5 = new CardModel(
                "Abhisek Mishra", "mishraabhi@gmail.com");
            employeeList.Add(emp5);
            CardModel emp6 = new CardModel("Sindhu Malhotra",
                                         "sindhu@gmail.com");
            employeeList.Add(emp6);
            CardModel emp7 = new CardModel("Anil sidhu",
                                         "sidhuanil@gmail.com");
            employeeList.Add(emp7);
            CardModel emp8 = new CardModel("Sachin sinha",
                                         "sinhas@gmail.com");
            employeeList.Add(emp8);
            CardModel emp9 = new CardModel("Amit sahoo",
                                         "sahooamit@gmail.com");
            employeeList.Add(emp9);
            CardModel emp10 = new CardModel("Raj kumar",
                                          "kumarraj@gmail.com");
            employeeList.Add(emp10);

            return employeeList;
        }
    }
}