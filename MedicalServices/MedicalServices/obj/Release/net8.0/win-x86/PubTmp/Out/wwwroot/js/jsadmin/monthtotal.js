const ctx = document.getElementById('myChart');
let myChart;
let locationpath = location.pathname;
let id = +locationpath[locationpath.length - 1];

initializeChart();
fetchChartData();

function initializeChart() {
    myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [], // Start with empty labels
            datasets: [{
                label: 'Monthly Gains',
                data: [], // Start with empty data
                borderWidth: 1,
                backgroundColor: 'rgba(54, 162, 235, 0.5)'
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

// Combined function to fetch both months and gains
function fetchChartData() {
    $.when(
        $.ajax({ url: `/Branch/GetMonths` }),
        $.ajax({ url: `/Branch/LastMonthesGain/${id}` })
    ).then(function (monthsResponse, gainsResponse) {
        try {
            // Process months
            const months = typeof monthsResponse[0] === 'string'
                ? JSON.parse(monthsResponse[0])
                : monthsResponse[0];

            // Process gains
            const gains = typeof gainsResponse[0] === 'string'
                ? JSON.parse(gainsResponse[0])
                : gainsResponse[0];


            // Update chart
            updateChart(months, gains);
        } catch (error) {
            console.error('Error processing data:', error);
        }
    }).fail(function (xhr, status, error) {
        console.error('AJAX request failed:', status, error);
    });
}

function updateChart(monthNames, gainValues) {
    myChart.data.labels = monthNames;
    myChart.data.datasets[0].data = gainValues;

    myChart.update();
}