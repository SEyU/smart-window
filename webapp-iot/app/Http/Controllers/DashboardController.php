<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Services\MongoServices;
use App\User;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Validator;
use Illuminate\Foundation\Auth\RegistersUsers;
use DB;
use Response;
use View;


class DashboardController extends Controller
{

    /*
     * Get last data from MongoDB
     */
    public function getDashboard(){


        $result = MongoServices::getDashboard();
/*
        $tint = $result['tint']['mensaje']->getData();

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




        return View::make('pages.dashboard', compact('tint', 'text', 'viento', 'toldo', 'ventana', 'humedad', 'modo', 'iluminacion'));
    }

}
