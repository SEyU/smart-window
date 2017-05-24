@extends('master')

@section('title', 'Viento')


@section('content')




<div class="row">
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                Gráfica de la velocidad del viento
            </div>
            <!-- /.panel-heading -->
            <div class="panel-body">
                <div class="flot-chart">
                    <div class="flot-chart-content" id="flot-line-chart"></div>
                </div>
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
</div>



@stop

@section('snippets-scripts')
    <!-- Flot Charts JavaScript -->
        <script src="{{asset('vendor/flot/excanvas.min.js')}}"></script>
        <script src="{{asset('vendor/flot/jquery.flot.js')}}"></script>
        <script src="{{asset('vendor/flot/jquery.flot.pie.js')}}"></script>
        <script src="{{asset('vendor/flot/jquery.flot.resize.js')}}"></script>
        <script src="{{asset('vendor/flot/jquery.flot.time.js')}}"></script>
        <script src="{{asset('vendor/flot-tooltip/jquery.flot.tooltip.min.js')}}"></script>
        {{--
        <script src="{{asset('data/flot-data.js')}}"></script>
--}}
        <script src="{{asset('js/flot-viento.js')}}"></script>

@stop