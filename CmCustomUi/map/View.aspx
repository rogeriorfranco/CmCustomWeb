<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="CmCustomUi.map.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mapa</title>
    <meta name="viewport" content="initial-scale=1.0" />
    <meta charset="utf-8" />
    <style>
        #map {
            position: absolute;
            height: 90%;
            width: 100%;
        }

        html, body {
            height: 272px;
            margin: 0;
            padding: 0;
        }

        #legend {
            font-family: Arial, sans-serif;
            background: rgba(255, 255, 255, 0.8);
            padding: 10px;
            margin: 10px;
            border: 3px solid #000;
        }

            #legend h3 {
                margin-top: 0;
            }

            #legend img {
                vertical-align: middle;
            }
    </style>
    <link href="../content/bootstrap.min.css" rel="stylesheet" />
</head>

<script src="../scripts/MeasureTool.min.js"></script>
<script src="../scripts/jQuery-2.1.3.min.js"></script>
<script src="../scripts/bootstrap.min.js"></script>
<body onload="showLegend();">

    <form id="form1" runat="server">
        <%
            if (IsOk)
            {%>
        <div>
            <label id="lblDistance">Distância Total: 0.0 m</label>
        </div>

        <div id="map"></div>
        <div id="legend" style="display: none">
            <h3>Legenda</h3>
        </div>

        <% }
            else
            {
        %>
        <div>
            <asp:Label ID="LblErrorMessage" runat="server" Text="Label"></asp:Label>
        </div>
        <% } %>

        <script type="text/javascript">
            var markers = [
                <asp:Repeater ID="rptMarkers" runat="server">
                    <ItemTemplate>
                        {
                            "title"         : '<%# Eval("Name") %>',
                            "lat"           : '<%# Eval("Latitude") %>',
                            "lng"           : '<%# Eval("Longitude") %>',
                            "description"   : '<%# Eval("Description") %>',
                            "type"          : '<%# Eval("TypeElement") %>',
                            "lengthMaxDrop" : '<%# Eval("LengthMaxDrop") %>'
                        }
                    </ItemTemplate>
                    <SeparatorTemplate>,</SeparatorTemplate>
                </asp:Repeater >
            ];
        </script>

        <script>
            var map, measureTool;
            var value = 0.0;
            var distance = "Distância Total: 0.0 m";

            function calcularRota() {
                var myLatLngStart = { lat: parseFloat(<%= LatOrigin %>), lng: parseFloat(<%= LngOrigin %>) };
                var myLatLngEnd = { lat: parseFloat(<%= LatDestiny %>), lng: parseFloat(<%= LngDestiny %>) };

                var request = {
                    origin: myLatLngStart,
                    destination: myLatLngEnd,
                    provideRouteAlternatives: true,
                    travelMode: google.maps.TravelMode.WALKING,

                };
                servicoDirecao.route(request, function (response, status) {
                    if (status == google.maps.DirectionsStatus.OK) {
                        exibirCaminhos.setDirections(response);
                    }
                });
            }

            function marcar() {
                var markPrevious;
                var circlePrevious;
                var circlePreviousInside;
                var url_string = window.location.href;
                var url = new URL(url_string);
                var filtro        = url.searchParams.get("filtro");
                var stdRadius     = url.searchParams.get("raio");
               

                if (filtro == null) {
                    filtro = "all";
                } 

                if (stdRadius == null)
                {
                    stdRadius = "250.0";
                }

                var radiusCTO = parseFloat(<%= RadiusCTO %>);

                filtro = filtro.trim().toLowerCase();

                var infoWindow = new google.maps.InfoWindow();

                var imageBase = "../images/map/";

                var iconDefault = {
                    url: imageBase + "pinblue.png",
                    scaledSize: new google.maps.Size(35, 35)
                };

                var iconCTO = {
                    url: imageBase + "pincto.png",

                };

                var iconCEO = {
                    url: imageBase + "pinceo.png",
                  
                };

                var iconST = {
                    url: imageBase + "pinst.png",
                 
                };

                var iconCTOFull = {
                    url: imageBase + "pinctofull.png",
               
                };

                var types = [ "ST", "CEO", "CTO", "CTO_FULL"];

                function getIcon(type) {
                    var ret;
                    switch (type) {
                        case "ST": ret =
                            {
                                icon: iconST,
                                caption: '<b>ST</b>  ( Sobra Técnica )'
                            }; break;
                        case "CEO": ret =
                            {
                                icon: iconCEO,
                                caption: '<b>CEO</b>  ( Caixa de Emenda Óptica )'
                            }
                            break;
                        case "CTO": ret =
                            {
                                icon: iconCTO,
                                caption: '<b>CTO</b> ( Caixa de Terminação Óptica )'
                            }; break;
                        case "CTO_FULL": ret =
                            {
                                icon: iconCTOFull,
                                caption: '<b>CTO</b> ( 100% ocupado )'
                            }; break;
                        default: ret = iconDefault; break;
                    }
                    return ret;
                }

                for (i = 0; i < markers.length; i++) {

                    var data = markers[i];

                    data.myLatLng

                    var myLatLng = { lat: parseFloat(data.lat), lng: parseFloat(data.lng) };

                    var marker = new google.maps.Marker({
                        position: myLatLng,
                        map: map,
                        icon: getIcon(data.type).icon,
                        title: data.title
                    });

                    if (data.type == "CTO" || data.type == "CTO_FULL") {
                        marker.tag = (data.type == "CTO") ? '#0000FF' : '#7F0000';
                        marker.lengthMaxDrop = data.lengthMaxDrop;

                        if (filtro == "cto") {

                            marker.addListener('rightclick', function () {
                                var radius = (parseFloat(this.lengthMaxDrop) > 0) ? parseFloat(this.lengthMaxDrop) : radiusCTO;

                                var circleCurrent = new google.maps.Circle({
                                
                                    strokeColor: this.tag,
                                    strokeOpacity: 0.05,
                                    strokeWeight: 2,
                                    fillColor: this.tag,
                                    fillOpacity: 0.1,
                                    center: this.position,
                                    radius: radius
                                });

                                var circleCurrentInside = new google.maps.Circle({

                                    strokeColor: this.tag,
                                    strokeOpacity: 0.0,
                                    strokeWeight: 0,
                                    fillColor: this.tag,
                                    fillOpacity: 0.1,
                                    center: this.position,
                                    radius: radius * 0.7
                                });


                                if (markPrevious == this) {
                                    circleCurrent.setMap(circlePrevious.getMap() != null ? null : map);
                                    circleCurrentInside.setMap(circlePreviousInside.getMap() != null ? null : map);
                                    
                                }
                                else {
                                    circleCurrent.setMap(map);
                                    circleCurrentInside.setMap(map);
                                }

                                if (circlePrevious != null) {
                                    circlePrevious.setMap(null);
                                    circlePreviousInside.setMap(null);
                                }

                                circlePrevious = circleCurrent;
                                circlePreviousInside = circleCurrentInside;
                                markPrevious = this;

                            });
                        }
                    }

                    (function (marker, data) {
                        google.maps.event.addListener(marker, "click", function (e) {
                            infoWindow.setContent(data.description);
                            infoWindow.open(map, marker);
                        });
                    })(marker, data);

                }
               

                var legend = document.getElementById('legend');
                for (var key in types) {
                    var type = getIcon(types[key]);
                    var name = type.caption;
                    var icon = type.icon.url;
                    var div = document.createElement('div');
                    div.innerHTML = '<img src="' + icon + '" width="10%" height="10%"> ' + name;
                    legend.appendChild(div);
                }

                $('#legend').click(function () {
                    $(this).slideUp();
                });

           
                map.controls[google.maps.ControlPosition.LEFT_BOTTOM].push(legend);
            }

            function initMap() {
                servicoDirecao = new google.maps.DirectionsService();
                exibirCaminhos = new google.maps.DirectionsRenderer();

                map = new google.maps.Map(document.getElementById('map'), {
                    center: { lat: <%= Latitude %>, lng: <%= Longitude %> },
                    zoom:  <%= Zoom %>,
                    scaleControl: true
                });

                exibirCaminhos.setMap(map);

                measureTool = new MeasureTool(map, {
                    showSegmentLength: true,
                    tooltip: true,
                    unit: MeasureTool.UnitTypeId.METRIC // metric ou imperial

                });

                measureTool.addListener('measure_start', () => {
                    console.log('started');
                distance = "Distância Total: 0.0 m";
                document.getElementById('lblDistance').innerHTML = distance;

            });
            measureTool.addListener('measure_end', (e) => {
                console.log('ended', e.result);
            distance = "Distância Total: 0.0 m";
            document.getElementById('lblDistance').innerHTML = distance;

            });
            measureTool.addListener('measure_change', (e) => {
                console.log('changed', e.result);
            distance = "Distância Total: ".concat(e.result.lengthText);
            document.getElementById('lblDistance').innerHTML = distance;
            });

            var exibeRota = "<%= ExibirRota %>";
                    marcar();

                    if (exibeRota == "True") {
                        calcularRota();
                    }
                    }
        </script>

        <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBLi2_43uMRr207MvZSxp5OaS-kio0IGac&libraries=geometry&callback=initMap"></script>
    </form>
    <script>
        function showLegend() {
            $("#legend").show();
        }
    </script>
</body>
</html>
