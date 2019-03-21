<?php
  $servername = 'localhost';
  $username = 'root';
  $password = '';
  $database = 'madra';

  $connection = mysqli_connect($servername, $username, $password, $database);

  $action = $_POST['action'];

  if($action == 'insert') {
    $table = $_POST['table'];
    $columns = $_POST['columns'];
    $values = $_POST['values'];

    $sql = "INSERT INTO ".$table."(".$columns . ") VALUES(" . $values.");";
    $result = mysqli_query($connection, $sql);
    $connection->close();
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
      $where = " WHERE " . $_POST['where'];
    }

    $sql = 'SELECT ' . $select . ' FROM ' . $_POST['from'] . $where . ';';

    $result = mysqli_query($connection, $sql);

    if(mysqli_num_rows($result) > 0) {
      $resultData = mysqli_fetch_all($result, MYSQLI_ASSOC);
      echo json_encode($resultData, JSON_FORCE_OBJECT);
    } else {
      echo "False";
    }

    $connection->close();
  }

  if($action == 'delete') {
  
    $sql = "DELETE FROM " . $_POST['from'] . " WHERE " . $_POST['where'] . ";";
    $result = mysqli_query($connection, $sql);
    $connection->close();
  }

?>
