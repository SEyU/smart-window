{{--

    Dashboard with the last data send by node mcu

--}}

@extends('master')

@section('title', 'Dashboard')

@section('snippets-scripts')

    <script src="{{asset('js/ajax-dashboard.js')}}"></script>


@stop


@section('content')


<div class="row">
    <div class="col-lg-3 col-md-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="wi wi-thermometer fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="Tint"> {!! $tint !!} </label> ºC</div>
                        <div>Tª interior</div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="col-lg-3 col-md-6">
        <div class="panel panel-green">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="wi wi-thermometer-exterior fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="Text" >{!! $text !!}</label> ºC</div>
                        <div>Tª exterior</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3 col-md-6">
        <div class="panel panel-yellow">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="wi wi-strong-wind fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><small><label id="Viento" >{!! $viento !!}</label>  Km/h</small></div>
                        <div>V. Viento</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3 col-md-6">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="wi wi-humidity fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="humedad" >{!! $humedad !!}</label> %</div>
                        <div>Humedad</div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-yellow">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="wi wi-day-sunny fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="Ilum" >{!! $iluminacion !!}</label> %</div>
                        <div>Iluminación</div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="div-auto" class="col-lg-3 col-md-6 {{ ( $modo == 1 )  ? 'hidden' :  ''}}">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-spinner fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div id='modo' class="huge">AUTO</div>
                        <div>Modo actual</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="div-man" class="col-lg-3 col-md-6 {{ ( $modo == 0 )  ? 'hidden' :  ''}}">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-hand-pointer-o fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div id="modo" class="huge">MANUAL</div>
                        <div>Modo actual</div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="col-lg-3 col-md-6">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-window-maximize fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="Ventana" >{!! $ventana !!}</label> %</div>
                        <div>Estado ventana</div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="col-lg-3 col-md-6">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-bookmark fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><label id="Toldo" >{!! $toldo !!}</label> %</div>
                        <div>Estado toldo</div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>

<div class="alert alert-success hidden" id="div-alert-update">
    Datos actualizados a las <strong><label id="info-actualizado"></label></strong>
</div>

@stop



