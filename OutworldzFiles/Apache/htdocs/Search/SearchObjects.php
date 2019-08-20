<?php
// AGPL 3.0 by Fred Beckhusen
require( "flog.php" );

include("databaseinfo.php");

 // Attempt to connect to the database
  try {
    $db = new PDO("mysql:host=$DB_HOST;port=$DB_PORT;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
  }
catch(PDOException $e)
{
  
  echo "Error connecting to database\n";
  file_put_contents('../../../PHPLog.log', $e->getMessage() . "\n-----\n", FILE_APPEND);
  exit;
}

    
?>


<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
        <style type="text/css">
            tr.header
            {
                font-weight:bold;
            }
            tr.alt
            {
                background-color: #dddddd;
            }
        </style>
        <script type="text/javascript">
            $(document).ready(function(){
               $('.striped tr:even').addClass('alt');
            });
        </script>
        <title>Search Objects</title>
        <link rel="shortcut icon" href="/favicon.ico">
    </head>
    <body>
        
        <table class="striped">
            <tr class="header">
                <td>objectuuid</td>
                <td>parceluuid</td>
                <td>location</td>
                <td>name</td>
                <td>description</td>
                <td>regionuuid</td>
            </tr>
            <?php
             
             
             $query = "SELECT * FROM objects order by name";
             
             $sqldata = array();
             
             $query = $db->prepare($query);
             $result = $query->execute($sqldata);
             $counter = 0;


               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                echo "<tr valign=\"top\">";
                 
                 echo "<td>" .$row["objectuuid"] . "</td>";
                 echo "<td>" .$row["parceluuid"] . "</td>";
                 echo "<td>" .$row["location"] . "</td>";
                 echo "<td>" . $row["name"] . "</td>";
                 echo "<td>" . $row["description"] . "</td>";
                 echo "<td>" . $row["regionuuid"] . "</td>";
                 echo "</tr>";
                 $counter+= 1;
               }
                if ($counter == 0) {
                echo "<tr valign=\"top\">";
                echo "<td> </td>";
                echo "<td>Nothing found</td>";
                echo "</tr>";
            }
            echo "</table>";
            echo "<input type=\"button\" value=\"Go Back\" onclick=\"history.back(-1)\" />"; 
            ?>
        </table>
    </body>
</html>