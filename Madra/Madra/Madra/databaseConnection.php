<?php
  $servername = 'localhost';
  $username = 'root';
  $password = '';
  $database = 'madra';

  $connection = mysqli_connect($servername, $username, $password, $database);

  $action = $_POST['action'];

  if($action == 'insert') {

  }

  if($action == 'update') {
    $table = $_POST['table'];
    $update = $_POST['update'];
    $where = $_POST['where'];

    $sql = "UPDATE ".$table." SET ".$update . " WHERE " . $where.";";
    $result = mysqli_query($connection, $sql);
    $connection->close();
  }

  if($action == 'select') {
    $where = '';
    $select = '*';

    if(isset($_POST['select'])) {
      $select = $_POST['select'];
    }

    if(isset($_POST['where'])) {
      $where = $_POST['where'];
    }

    $sql = 'SELECT ' . $select . ' FROM ' . $_POST['from'] . ' ' . $where . ';';

    $result = mysqli_query($connection, $sql);
    $resultData = mysqli_fetch_all($result, MYSQLI_ASSOC);
    echo json_encode($resultData, JSON_FORCE_OBJECT);
    $connection->close();
  }

  if($action == 'delete') {

  }

?>
