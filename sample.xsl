<?xml version="1.0" encoding="UTF-8"?>
<html xsl:version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <link rel="icon" href="https://www.w3schools.com/favicon.ico" type="image/x-icon" />
    <meta name="theme-color" content="#ffffff" />
    <meta name="description" content="Sorry! We can't seem to find the resource you're looking for" /> 
    <title>404 - Page not found | W3Schools.com</title>
  </head>
  <body>
<xsl:for-each select="pdf2xml/page">
  <div class="page" style="height:400px">

  <input type="hidden"><xsl:value-of select="@height" /></input>
  <h1>Page <xsl:value-of select="@number" /></h1>
  <xsl:for-each select="text">
  <span><xsl:value-of select="text()" /></span>
    
  </xsl:for-each>
  </div>
</xsl:for-each>
<script type="javascript" src="sample.js">
</script>
	</body>
</html>