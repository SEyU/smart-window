<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Services\MongoServices;
use App\User;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Validator;
use Illuminate\Foundation\Auth\RegistersUsers;
use DB;
use Response;



class ApiController extends Controller
{
    /*
    |--------------------------------------------------------------------------
    | Api Controller
    |--------------------------------------------------------------------------
    |
    | This controller does query to mongodb of IoT
    |
    */


    public function getDashboard(){

        $result = MongoServices::getDashboard();

/*        $tint = $result['tint']['mensaje']->getData();

        $text = $result['text']['mensaje']->getData();

        $viento = $result['viento']['mensaje']->getData();
        $toldo = $result['toldo']['mensaje']->getData();
        $ventana = $result['ventana']['mensaje']->getData();
        $humedad = $result['humedad']['mensaje']->getData();
        $modo = $result['modo']['mensaje']->getData();
        $iluminacion = $result['iluminacion']['mensaje']->getData();*/

        $tint = $result['tint']['mensaje'];

        $text = $result['text']['mensaje'];

        $viento = $result['viento']['mensaje'];
        $toldo = $result['toldo']['mensaje'];
        $ventana = $result['ventana']['mensaje'];
        $humedad = $result['humedad']['mensaje'];
        $modo = $result['modo']['mensaje'];
        $iluminacion = $result['iluminacion']['mensaje'];

        return Response::json(  compact('tint', 'text', 'viento', 'toldo', 'ventana', 'humedad', 'modo', 'iluminacion') );
    }


    public function getTemperatureChart(){

        $topics = ["SmartWindow/MCUEXT/Clima/Temperatura", "SmartWindow/MCUINT/Clima/Temperatura"];
        $temperaturas = array();

        foreach ($topics as $topic){

            $opts = array(
                ['$match' => [ 'topic' =>  $topic ] ],
                ['$sort' => [ 'date' => -1]],
                ['$limit' => 45],
                ['$project' => ['_id' => 0, 'x' => '$date', 'y' => '$mensaje' ] ],
            );
            $db = DB::getMongoDB();
            $datos = $db->smartwindows->aggregate($opts);

            $aux = array();
            foreach ( $datos as $d) {
                $aux[] = [$d->x->__toString(), $d->y];
//                $aux[] = [$d->x->__toString(), $d->y->getData()];

            }
            $temperaturas[] = $aux;
        }

        return Response::json($temperaturas);

    }


    public function getWindChart(){


        $opts = array(
            ['$match' => [ 'topic' => 'SmartWindow/MCUEXT/Clima/VelViento' ] ],
            ['$sort' => [ 'date' => -1]],
            ['$limit' => 70],

            ['$project' => ['_id' => 0, 'x' => '$date', 'y' => '$mensaje' ] ],

        );


        $db = DB::getMongoDB();


        $datos = $db->test->aggregate($opts);

        $datos = $db->smartwindows->aggregate($opts);


        $temperaturas = array();
        foreach ($datos as $d ){
            $temperaturas[] = [$d->x->__toString(), $d->y];
        //    $temperaturas[] = [$d->x->__toString(), $d->y->getData()];

        }


        return Response::json($temperaturas);

    }



}
