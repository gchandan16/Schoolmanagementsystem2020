google.charts.load("current", { packages: ["corechart"] });

function DrawOrderSale(chartid) {
    var data = google.visualization.arrayToDataTable([
        ["Element", "Rs", { role: "style" }],
        ["Budget", 2032, "rgb(66, 133, 244)"],
        ["Net Order", 1388, "rgb(15, 157, 88)"],
        ["Net Sale + DA", 1203, "rgb(244, 180, 0)"],
        ["Pending After DA", 165, "rgb(219, 68, 55)"],
        ["Due to Stock", 93, "rgb(171, 71, 188)"]
    ]);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        },
        2]);

    var options = {
        title: "",
        height: 200,
        bar: { groupWidth: "50%" },
        legend: { position: "none" },
    };
    var chart = new google.visualization.ColumnChart(document.getElementById(chartid));
    chart.draw(view, options);

    var chart1 = new google.visualization.ColumnChart(document.getElementById(chartid + "_1"));
    chart1.draw(view, options);

    var chart2 = new google.visualization.ColumnChart(document.getElementById(chartid + "_2"));
    chart2.draw(view, options);

}


function drawSegmentwiseBudget(chartid) {
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Achievement', 80],

    ]);

    var options = {
        height: 160,
        redFrom: 0, redTo: 20,
        yellowFrom: 20, yellowTo: 60, greenFrom: 60, greenTo: 100,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById(chartid));

    chart.draw(data, options);

}

function drawAreawiseBudget(chatid) {
    var data = google.visualization.arrayToDataTable([
        ['Area', 'TARGET', 'NETORDER', 'NETSALE', 'PENDING ORDER'],
        ['EAST', 388, 233, 201, 31],
        ['WEST', 1170, 460, 250, 43],
        ['NORTH', 660, 1120, 300, 55],
        ['SOUTH', 1030, 540, 350, 66]
    ]);

    var options = {
        chart: {
            title: '',
            colors: ['rgb(66, 133, 244)', 'rgb(15, 157, 88)', 'rgb(244, 180, 0)', 'rgb(219, 68, 55)'],
            is3D: true
        }
    };

    var chart = new google.charts.Bar(document.getElementById(chatid));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}

function drawCategorywiseTable(chartid) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'PRODUCT');
    data.addColumn('number', 'TARGET');
    data.addColumn('number', 'ORDER-PENDING');
    data.addColumn('number', 'ORDER-CURRENT');
    data.addColumn('number', 'DIN-CURRENT');
    data.addColumn('number', 'SALE-INVOICE');
    data.addColumn('number', 'ORD vz INV EXE(%)');
    data.addRows([
        ['LIGHT', { v: 2000, f: '$10,000' }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['MIRROR', { v: 8000, f: '$8,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['FILTER', { v: 12500, f: '$12,500' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['HORN', { v: 7000, f: '$7,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['ELECTRICAL', { v: 7000, f: '$7,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['CABLE', { v: 7000, f: '$7,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['BULB', { v: 7000, f: '$7,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['OTHER', { v: 7000, f: '$7,000' },  { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }],
        ['TOTAL', { v: 7000, f: '$7,000' }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }, { v: 2000 }]
    ]);

    var table = new google.visualization.Table(document.getElementById(chartid));

    table.draw(data, { showRowNumber: true, width: '100%', height: '100%' });
}