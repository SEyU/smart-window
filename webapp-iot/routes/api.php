<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});


Route::get('/ajax/dashboard', ['as' => 'api-dashboard', 'uses'  => 'ApiController@getDashboard']);

Route::get('/ajax/tempchart', ['as' => 'api-temp-chart', 'uses'  => 'ApiController@getTemperatureChart']);


Route::get('/ajax/windchart', ['as' => 'api-wind-chart', 'uses'  => 'ApiController@getWindChart']);
