<?php
	header('Content-Type: application/json');
	$ip = getenv("REMOTE_ADDR");
	$data = array('ip' => "$ip");
	$json = json_encode($data);

	print "getip($json)";
?> 