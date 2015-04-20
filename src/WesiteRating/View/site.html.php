<html>
    <head>
        <title>Рейтинг популярности</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
        <link rel="stylesheet" href="//rawgit.com/carlosrocha/react-data-components/master/css/table-twbs.css">
        <link rel="stylesheet" href="css/style.css">
    </head>
    <body>
        <? if($stats) {
            echo "<pre>"; var_dump($stats);
        } else { ?>

        <script src="build/bundle.js"></script>

        <? } ?>
    </body>
</html>
