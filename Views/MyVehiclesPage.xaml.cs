using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PRN212.Models;

namespace PRN212.Views;

public partial class MyVehiclesPage : Page
{
    public MyVehiclesPage()
    {
        InitializeComponent();
        LoadDataGrid();
        LoadComboModel();
        LoadComboBrand();
        LoadComboYear();    
    }
    void LoadDataGrid()
    {
        User currentUser = (User)Application.Current.Properties["User"];
        Prn212Context context = new Prn212Context();
        var veh = context.Vehicles
            .Include(v => v.Owner)
            .Where(v => v.OwnerId == currentUser.UserId)
            .ToList();
        this.dgVehicles.ItemsSource = veh;
    }
    void LoadComboModel()
    {
        User currentUser = (User)Application.Current.Properties["User"];
        Prn212Context context = new Prn212Context();
        var veh = context.Vehicles
            .Where(v => v.OwnerId == currentUser.UserId)
            .ToList()
            .GroupBy(v => v.Model);
        this.cmbModel.ItemsSource = veh;
        this.cmbModel.DisplayMemberPath = "Model";
        this.cmbModel.SelectedValuePath = "Model";
        this.cmbModel.SelectedIndex = -1;
    }
    void LoadComboBrand()
    {
        User currentUser = (User)Application.Current.Properties["User"];
        Prn212Context context = new Prn212Context();
        var veh = context.Vehicles
            .Where(v => v.OwnerId == currentUser.UserId)
            .ToList()
            .GroupBy(v => v.Brand);
        this.cmbBrand.ItemsSource = veh;
        this.cmbBrand.DisplayMemberPath = "Brand";
        this.cmbBrand.SelectedValuePath = "Brand";
        this.cmbBrand.SelectedIndex = -1;
    }
    void LoadComboYear()
    {
        User currentUser = (User)Application.Current.Properties["User"];
        Prn212Context context = new Prn212Context();
        var veh = context.Vehicles
            .Where(v => v.OwnerId == currentUser.UserId)
            .ToList()
            .GroupBy(v => v.ManufactureYear);
        this.cmbYear.ItemsSource = veh;
        this.cmbYear.DisplayMemberPath = "ManufactureYear";
        this.cmbYear.SelectedValuePath = "ManufactureYear";
        this.cmbYear.SelectedIndex = -1;
    }

    private void btnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        User currentUser = (User)Application.Current.Properties["User"];
        Prn212Context context = new Prn212Context();
        IQueryable<Vehicle> query = context.Vehicles
            .Where(v => v.OwnerId == currentUser.UserId)
            .Include(v => v.Owner);

        if (cmbBrand.SelectedItem != null)
        {
            string selectedBrand = cmbBrand.SelectedValue.ToString();
            query = query.Where(v => v.Brand == selectedBrand);
        }

        if (cmbModel.SelectedItem != null)
        {
            string selectedModel = cmbModel.SelectedValue.ToString();
            query = query.Where(v => v.Model == selectedModel);
        }

        if (cmbYear.SelectedItem != null)
        {
            int selectedYear = Convert.ToInt32(cmbYear.SelectedValue);
            query = query.Where(v => v.ManufactureYear == selectedYear);
        }

        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            query = query.Where(v =>
                v.PlateNumber.ToLower().Contains(searchText) ||
                v.Brand.ToLower().Contains(searchText) ||
                v.Model.ToLower().Contains(searchText) ||
                v.Owner.FullName.ToLower().Contains(searchText)
            );
        }

        var results = query.ToList();
        dgVehicles.ItemsSource = results;
    }

    private void btnReset_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        cmbBrand.SelectedIndex = -1;
        cmbModel.SelectedIndex = -1;
        cmbYear.SelectedIndex = -1;
        txtSearch.Text = string.Empty;

        LoadDataGrid();
    }
}