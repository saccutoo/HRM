 var endYear = new Date(new Date().getFullYear(), 11, 31);
       $("#datePicker").datepicker({
        autoclose: true,
        format: "mm/yyyy",
        startDate: "1/2013",
        endDate: endYear,
        startView: "months",
        minViewMode: "months",
        maxViewMode: "years"
       }).datepicker("setDate", new Date());
    $("#datePicker2").datepicker({
        autoclose: true,
        format: "mm/yyyy",
        startDate: "1/2013",
        endDate: endYear,
        startView: "months",
        minViewMode: "months",
        maxViewMode: "years"
    }).datepicker("setDate", new Date());
    $("#datePicker3").datepicker({
        autoclose: true,
        format: "mm/yyyy",
        startDate: "1/2013",
        endDate: endYear,
        startView: "months",
        minViewMode: "months",
        maxViewMode: "years"
    }).datepicker("setDate", new Date());