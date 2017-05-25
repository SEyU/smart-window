<?php


namespace App\Http\Controllers\Services;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\DB;

class MongoServices extends Controller
{
    public static function getDashboard(){

        $tint = self::getLastTint();

        $text = self::getLastText();

        $viento = self::getLastWind();


        $toldo = self::getLastToldo();

        $ventana = self::getLastVentana();


        $humedad = self::getLastHumedad();

        $modo = self::getLastMode();


        $iluminacion = self::getLastIluminacion();



        return compact('tint', 'text', 'viento', 'toldo', 'ventana', 'humedad', 'modo', 'iluminacion');
    }


    public static function getLastTint(){

        $tint = DB::connection('mongodb')->collection('test')->where('topic', 'SmartWindow/MCUINT/Clima/Temperatura')->orderBy('date', 'desc')->first();

        $tint = DB::connection('mongodb')->collection('smartwindows')->where('topic', 'SmartWindow/MCUINT/Clima/Temperatura')->orderBy('date', 'desc')->first();

        return $tint;
    }



    public static function getLastText(){

        $text = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/Clima/Temperatura')->orderBy('date', 'desc')->first();

        $text = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/Clima/Temperatura')->orderBy('date', 'desc')->first();



        return $text;
    }


    public static function getLastWind(){

        $wind = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/Clima/VelViento')->orderBy('date', 'desc')->first();

        $wind = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/Clima/VelViento')->orderBy('date', 'desc')->first();

        return $wind;
    }

    public static function getLastToldo(){

        $toldo = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/Toldo/Estado')->orderBy('date', 'desc')->first();


        $toldo = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/Toldo/Estado')->orderBy('date', 'desc')->first();

        return $toldo;
    }


    public static function getLastVentana(){

        $ventana = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/Ventana/Estado')->orderBy('date', 'desc')->first();

        $ventana = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/Ventana/Estado')->orderBy('date', 'desc')->first();


        return $ventana;
    }

    public static function getLastHumedad(){

        $humedad = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUINT/Clima/Humedad')->orderBy('date', 'desc')->first();

        $humedad = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUINT/Clima/Humedad')->orderBy('date', 'desc')->first();

        return $humedad;
    }


    public static function getLastMode(){

        $mode = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/ModoManual')->orderBy('date', 'desc')->first();

        $mode = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/ModoManual')->orderBy('date', 'desc')->first();

        return $mode;
    }


    public static function getLastIluminacion(){

        $iluminacion = DB::connection('mongodb')->collection('test')->where('topic','SmartWindow/MCUEXT/Clima/Iluminacion')->orderBy('date', 'desc')->first();

        $iluminacion = DB::connection('mongodb')->collection('smartwindows')->where('topic','SmartWindow/MCUEXT/Clima/Iluminacion')->orderBy('date', 'desc')->first();

        return $iluminacion;
    }


}