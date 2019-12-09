<?php
$ToDo = simplexml_load_file("list.xml");
echo $ToDo -> list[0] -> attributes() ->id;
$counter = 0;
function searchObjectsByName($query){
    global $ToDo;
    $result = array();
    foreach ($ToDo -> list as $list1){
        if (substr(strtolower($list1 -> name), 0, strlen($query))== strtolower($query))
            array_push($result, $list1);
    }
    return $result;

}
function searchObjectbyDate($query){
    global $ToDo;
    $result = array();
    foreach ($ToDo -> list as $list1){
        if (substr(strtolower($list1 -> date), 0, strlen($query))== strtolower($query))
            array_push($result, $list1);
    }
    return $result;

}
?>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">  
    <title>ToDo</title>
    <meta charset="utf-8">
	
</head>
<body>
<h1 class="text-center">ToDo List</h1>
<h3 class="text-center">Table</h3>
	<table id="myTable" border="1" class="table table-hover table-dark">
				<tr>
					<th scope="col">name</th>
					<th scope="col">date</th>
					<th scope="col">description</th>
					<th scope="col">period</th>

				</tr>
				
				<?php	  
					foreach($ToDo -> list as $list1){
					$counter++;
					echo "<tr>";
					echo "<td>".($list1 -> name)."</td>";
					echo "<td>".($list1 -> date)."</td>";
					echo "<td>".($list1 -> description)."</td>";
					echo "<td>".($list1 -> period)."</td>";
					echo "<tr>";
					}
					echo "</br>";
					echo "All objects: ".($counter) 
					
				
				?>
			</table>
			<br /> 
        
        <form action="?" method="post">
            Search: <input type="text" name="searchName" placeholder="Name"/>
            <input type="submit" value="Find" />
        </form>
        <table border="1" class="table table-hover table-dark">
            <tr>
                <th scope="col">name</th>
                <th scope="col">date</th>
                <th scope="col">description</th>
				<th scope="col">period</th>
                
            </tr>
            <?php
            if(!empty($_POST["searchName"])){
            $result = searchObjectsByName($_POST["searchName"]);
            foreach($result as $list1) {
                echo "<tr>";
                echo "<td>".($list1 -> name)."</td>";
                echo "<td>".($list1 -> date)."</td>";
                echo "<td>".($list1 -> description)."</td>";
				echo "<td>".($list1 -> period)."</td>";
                echo "</tr>";
                }
            }
            ?>
			
			
			
			
			
			</table>
			
			<form action="?" method="post">
            Search: <input type="text" name="searchDate" placeholder="Name"/>
            <input type="submit" value="Find" />
        </form>
			
			<table border="1" class="table table-hover table-dark">
            <tr>
                <th scope="col">name</th>
                <th scope="col">date</th>
                <th scope="col">description</th>
				<th scope="col">period</th>
                
            </tr>
			<?php
			if(!empty($_POST["searchDate"])){
				$result = searchObjectbyDate($_POST["searchDate"]);
            foreach($result as $list1) {
                echo "<tr>";
                echo "<td>".($list1 -> name)."</td>";
                echo "<td>".($list1 -> date)."</td>";
                echo "<td>".($list1 -> description)."</td>";
				echo "<td>".($list1 -> period)."</td>";
                echo "</tr>";
                }
			}
			
			
			?>
			</table>
</body>
</html>
