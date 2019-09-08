<?php
// AGPL 3.0 by Fred Beckhusen
require( "flog.php" );

include("databaseinfo.php");
include("../Metromap/includes/config.php");
  
  
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
     
  $text = $_GET['query'];
  $text = "%$text%";
  $sqldata['text1'] = $text;
  

  $rc = intval($_GET['rp']);
   
  if ($rc == "") {
      $rc = 100;
  }
  

  $sort = $_GET['sortname'];
  if ($sort == 'Name') {
    $sort = 'Name';
  }else{
    $sort = 'Description';
  }
  
  $ord = $_GET['sortorder'];
  if ($ord == 'asc') {
    $ord = 'asc';
  } else {
    $ord = 'desc';
  }
  
  $qtype = $_GET['qtype'];
  if ($qtype == 'Name') {
    $qtype = 'Name';
  } else {
    $qtype = 'Description';
  }
  
  flog("text= $text");
  flog("qtype= $qtype");
  flog("ord= $ord");
  flog("sort= $sort");
  
  $total = 0;
   
  $page =  $_GET['page'];
  if ($page == "" ) {
    $page = 1;
  }
  
  $stack = array();
//left  JOIN hostsregister ON  hostsregister.gateway  =Regions.gateway 
  $q = "SELECT Regions.gateway as AGateway, Name, Description, Location, Regions.Regionname as  Regioname FROM Objects
    left  JOIN Regions ON Objects.regionuuid = Regions.regionuuid
    
            where
            Regions.gateway not like 'http://192.168%'
            and Regions.gateway not like 'http://172.16%'
            and Regions.gateway not like 'http://172.17%'
            and Regions.gateway not like 'http://172.18%'
            and Regions.gateway not like 'http://172.19%'
            and Regions.gateway not like 'http://172.20%'
            and Regions.gateway not like 'http://172.21%'
            and Regions.gateway not like 'http://172.22%'
            and Regions.gateway not like 'http://172.23%'
            and Regions.gateway not like 'http://172.24%'
            and Regions.gateway not like 'http://172.25%'
            and Regions.gateway not like 'http://172.26%'
            and Regions.gateway not like 'http://172.27%'
            and Regions.gateway not like 'http://172.28%'
            and Regions.gateway not like 'http://172.29%'
            and Regions.gateway not like 'http://172.30%'
            and Regions.gateway not like 'http://172.31%'            
            and Regions.gateway <> 'http://127.0.0.1'
            and Regions.gateway not like 'http://10.%'
            and " . $qtype . "  like CONCAT('%', :text1, '%')
            order by " . $sort . ' ' .  $ord ;
    

    flog($q );  
    $query = $db->prepare($q);
    $result = $query->execute($sqldata);
    flog($sqldata );  
    class OUT {}
    class Row {}
    
    $out = new OUT();
    $counter= 0;
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
      
        $location = $row["Location"];
        $v3    = "secondlife:///app/teleport/" . $row["AGateway"] . '/' . $location;     
        $local = "secondlife:///app/teleport/" . $row["AGateway"] . '/' . $location ;     
        
        $link = "<a href=\"$v3\"><img src=\"v3hg.png\" height=\"24\"></a>";
              
        $description = wordwrap($row["Description"],30, "<br>\n", false);
        $name = wordwrap($row["Name"],35, "<br>\n", false);
        
        $row = array("hop"=>$link ,
                     "Name"=>$name,
                     "Description"=>$description,
                     "Regionname"=>$row["Regioname"]. "<br>Link: <br><a href=\"$v3\">" . $row["AGateway"] . '/' . $location . '</a>',
                     "Location"=>$location);
        
        $rowobj = new Row();
        $rowobj->cell = $row;
        
        #$myJSON = json_encode($rowobj);
        #echo $myJSON . "<br>";
        if ($total >= (($page-1) *$rc) && $total < ($page) *$rc) {
          array_push($stack, $rowobj);
        }
        $total++;
        
    }
    if ($total == 0) {
	  flog("Nothing found");
      $row = array("hop"=>"", "Name"=>"No records","Description"=>"No records","Regionname"=>"No records","Location"=>"No records");
      $rowobj = new Row();
      $rowobj->cell = $row;
      array_push($stack, $rowobj);
    }

    $out->domain = $CONF_domain;
    $out->port = $CONF_port;
    $out->page  = $page;
    $out->total = $total;
    $out->rows  = $stack;
        
    $myJSON = json_encode($out);

    echo $myJSON;
   
   
?>
