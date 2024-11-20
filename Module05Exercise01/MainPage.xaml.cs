﻿using Module05Exercise01.Services;
using MySql.Data.MySqlClient;

namespace Module05Exercise01
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly DatabaseConnectionService _dbConnectionService;

        public MainPage()
        {
            InitializeComponent();

            _dbConnectionService = new DatabaseConnectionService();
        }

        private async void OpenViewEmployee(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ViewEmployee");
        }
    }
}
