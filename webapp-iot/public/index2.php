
<?php
require '../vendor/autoload.php'; // incluir lo bueno de Composer

$cliente = new MongoDB\Client("mongodb://192.168.0.167:27017");
$colección = $cliente->iot->mensajes;

$resultado = $colección->insertOne( [ 'name' => 'Hinterland', 'brewery' => 'BrewDog' ] );

echo "Inserted with Object ID '{$resultado->getInsertedId()}'";
?>
