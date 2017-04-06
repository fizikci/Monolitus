<%@ Page Title="" Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Monolitus.API" %>
    <%
        ApiJson service = new ApiJson();
        int index = 0;

        Dictionary<string, string> requestSamplesJSON = new Dictionary<string, string>();
        foreach (MethodInfo mi in service.GetServiceMethods())
        {
            if (!requestSamplesJSON.ContainsKey(mi.Name))
                requestSamplesJSON.Add(mi.Name, service.GetServiceMethodRequestSample(mi, "json"));
        }
    %>

<div class="page-header">
    <h1>API Demo
		   
        <small>
            <i class="icon-double-angle-right"></i>
            to learn how to use
		</small>
    </h1>
</div>

<script>
    var requestSamples = <%=Newtonsoft.Json.JsonConvert.SerializeObject(requestSamplesJSON)%>;
                                    
    function showTest(methodName)
    {
        if(methodName){
            $('#txtMethod').val(methodName);
            $('#txtData').val(requestSamples[methodName]);
        } else {
            $('#txtMethod').val('');
            $('#txtData').val('');
        }
        $('#txtServiceResponse').val('');
    }

    function runTest(){
        var data = $('#sampleForm').serialize();

        $.ajax({
            url: "/ApiJson.ashx",
            data: data,
            type: 'POST',
            dataType: 'json',
            success: function(data) {
                data = JSON.stringify(data, null, '\t');
                $('#txtServiceResponse').val(data);
            },
            error: function(err){
                alert(JSON.stringify(err, null, '\t'));
            }
        });
    }
</script>

<form onsubmit="return false" id="sampleForm">
    API Method:
    <select id="txtMethod" name="method" onchange="showTest($('#txtMethod').val())">
        <option value="">Select</option>
        <%foreach (MethodInfo mi in service.GetServiceMethods().OrderBy(m => m.Name))
            { %>
        <option value="<%=mi.Name%>"><%=mi.Name%></option>
        <%} %>
    </select><br />
    <textarea id="txtData" name="data" style="display: block; width: 97%; height: 200px"></textarea>
    <p align="center"><button class="btn btn-primary" onclick="runTest()" onclick="">Send request to <span style="color: #0f79c6">/ApiJson.ashx</span></button></p>
    <textarea id="txtServiceResponse" style="display: block; width: 97%; height: 200px"></textarea>
</form>