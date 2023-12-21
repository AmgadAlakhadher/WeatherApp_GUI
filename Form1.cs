// using System;
// using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using System.Drawing;
namespace WeatherApp;

public partial class Form1 : Form
{
    private TextBox cityTb;
    public Form1()
    {
        // Initialize TextBox
        cityTb = new TextBox();
        //elements
        Label cityLb = new Label(){
            Text = "Введите город для получения информации о погоде:",
            Location = new Point(this.Width / 2, this.Height / 2 ),
            Width =  500,
        };
        cityTb.Location = new Point(this.Width / 2, (this.Height / 2) + 30 );
        cityTb.Width =  500;
        
        Button searchBtn = new Button(){
            Text = "искать",
            Location = new Point(this.Width / 2, (this.Height / 2) + 60 ),
            Width =  500,
            Height = 30,
        };
        Button closeBtn = new Button(){
            Text = "выключать",
            Location = new Point(this.Width / 2, (this.Height / 2) + 90 ),
            Width =  500,
            Height = 30,
        };

        //Events
        closeBtn.Click +=  CloseBtn_Click;
        searchBtn.Click += SearchBtn_ClickAsync;
        cityTb.Focus();
        //add to current form
        this.Controls.Add(cityLb);
        this.Controls.Add(cityTb);
        this.Controls.Add(searchBtn);
        this.Controls.Add(closeBtn);
        InitializeComponent();
    }

    //search
    private async void SearchBtn_ClickAsync(object? sender, EventArgs e)
    { 
        if (!string.IsNullOrEmpty(cityTb.Text))
        {
            try
            {
                var weatherData = await GetWeatherData(cityTb.Text);

                MessageBox.Show($"Город: {weatherData.name}\nТемпература: {weatherData.main.temp}°C\nОписание: {weatherData.weather[0].description}\nСкорость ветра: {weatherData.wind.speed} м/с");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Название города не может быть пустым.");

        }
    }

    //close app
    private void CloseBtn_Click(object? sender, EventArgs e) 
    {
        this.Close();
    }
    protected void City_TextChanged(object sender, EventArgs e) 
    {
    }
      static async Task<WeatherData> GetWeatherData(string city)
    {
        try
        {
            var requestUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=c4da20a20e511c0b7466479ae33f29aa&units=metric";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonSerializer.Deserialize<WeatherData>(json);
                    return weatherData;
                }
                else
                {
                    MessageBox.Show($"Ошибка: {response.StatusCode.ToString()}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new SystemException("Произошла ошибка при получении данных о погоде.", ex);
        }
    }
    
}

class WeatherData
{
    public string name { get; set; }
    public Main main { get; set; }
    public Weather[] weather { get; set; }
    public WindData wind { get; set; }
}

class WindData
{
    public double speed { get; set; }
}

class Weather
{
    public string description { get; set; }
}
class Main
{
    public double temp { get; set; }
}
