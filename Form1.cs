using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuotesAPIClient
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private int numberOfApiCalls;
        public Form1()
        {
            InitializeComponent();
            numberOfApiCalls = 0;
        }
        private static async Task<ApiResponse> ProcessInspiringQuote()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = client.GetStreamAsync("https://quotes.rest/qod.json?category=inspire");
            var quoteTask = await JsonSerializer.DeserializeAsync<ApiResponse>(await streamTask);
            return quoteTask;
        }
        private async void callApiBtn_Click(object sender, EventArgs e)
        {
            var response = await ProcessInspiringQuote();
            updateForm(response);
            numberOfApiCalls++;
        }
        private void updateForm(ApiResponse response)
        {
            foreach (KeyValuePair<string, List<Quotes>> entry in response.contents)
            {
                string key = entry.Key;
                List<Quotes> quotes = entry.Value;
                quotes.ForEach(quote =>
                {
                    label1.Text = quote.quote;
                    label2.Text = $"By {quote.author}";
                    label3.Text = $"Available here: {quote.permalink.ToString()}";
                });
            }
        }
    }
}
