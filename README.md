# Simple-Static-Site-Generator

Simple static site generator has one feature. 
If you can split your page design to 3 pieces.
Like, header | content | footer, than you can use this generator.

Its algorithm is something like this:
* traverses the directories and files
* If directory contains header.txt set header
* If directory contains footer.txt set footer
* For each .html file in the directory
  * Concat the contents of header + file + footer
  * Save the result to the output directory

example directory structure:
```
wwwroot/
    header.txt
    footer.txt
    index.html
    about.html
    _output/ (contents of this folder will be generated)
      index.html
      about.html
```

example command usage:
```
simpleSiteGenerator.exe --rootDir ./
simpleSiteGenerator.exe --rootDir ... --outputDir ...
```
