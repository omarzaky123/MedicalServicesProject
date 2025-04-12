let myPieChart; // Declare chart variable outside to make it accessible

document.addEventListener('DOMContentLoaded', function () {
    const ctx = document.getElementById('myPieChart').getContext('2d');
    let locationpath = location.pathname;
    let id = +locationpath[locationpath.length - 1];

    // Initialize chart with empty data first
    myPieChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: [], // Empty initially
            datasets: [{
                data: [], // Empty initially
                backgroundColor: [
                    'rgba(255, 99, 132, 0.7)',
                    'rgba(54, 162, 235, 0.7)',
                    'rgba(255, 206, 86, 0.7)',
                    'rgba(75, 192, 192, 0.7)',
                    'rgba(153, 102, 255, 0.7)',
                    // Add more colors if you expect more than 5 services
                    'rgba(255, 159, 64, 0.7)',
                    'rgba(199, 199, 199, 0.7)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Most Used Services'
                }
            }
        }
    });

    // Fetch data after chart initialization
    AjaxCallMostService(id);
});

function AjaxCallMostService(id) {
    $.ajax({
        url: `/Branch/GetMostServices/${id}`,
        data: {},
        success: function (response) {
           

            // Extract labels and data from response
            const labels = [];
            const data = [];

            // Assuming response is an array of objects with servicesName and servicesCount
            response.forEach(item => {
                labels.push(item.servicesName);
                data.push(item.servicesCount);
            });

            // Update chart data
            myPieChart.data.labels = labels;
            myPieChart.data.datasets[0].data = data;

            // Update chart title if needed
            myPieChart.options.plugins.title.text = 'Most Used Services';

            // Refresh the chart
            myPieChart.update();
        },
        error: function (xhr, status, error) {
            console.error("Error fetching service data:", error);
        }
    });
}