﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _4_feladat
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        public Form1()
        {
            InitializeComponent();
            Loaddata();
        }
        private void Loaddata()
        {
            Flats = context.Flats.ToList();
        }
    }
}
