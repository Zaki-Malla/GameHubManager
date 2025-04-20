const doughnutRoundedChartInit = () => {
    const { getColor } = window.phoenix.utils;
    const chartElements = document.querySelectorAll('.echart-doughnut-rounded-chart-example');

    chartElements.forEach((chartElement, index) => {
        const chartInstance = window.echarts.init(chartElement);

        const chartData = getChartDataByIndex(index); 

        const options = {
            legend: {
                orient: 'vertical',
                left: 'left',
                textStyle: {
                    color: getColor('gray-600')
                }
            },
            series: [
                {
                    type: 'pie',
                    radius: ['40%', '70%'],
                    center: window.innerWidth < 530 ? ['65%', '55%'] : ['50%', '55%'],
                    avoidLabelOverlap: false,
                    itemStyle: {
                        borderRadius: 10,
                        borderColor: getColor('gray-100'),
                        borderWidth: 2
                    },
                    label: {
                        show: false,
                        position: 'center'
                    },
                    labelLine: {
                        show: false
                    },
                    data: chartData
                }
            ],
            tooltip: {
                trigger: 'item',
                padding: [7, 10],
                backgroundColor: getColor('gray-100'),
                borderColor: getColor('gray-300'),
                textStyle: {
                    color: getColor('dark')
                },
                borderWidth: 1,
                transitionDuration: 0,
                axisPointer: {
                    type: 'none'
                }
            }
        };

        chartInstance.setOption(options);
    });
};
