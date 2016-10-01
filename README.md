# C# Text Permutator
What does it do?
- It takes input line by line from a text file, finds and lists all distinct permutations of that text, and then writes them to an output file
---
How can I test it?
- You'd need to open this up in Visual Studio and run it with CTRL+F5
- To change the input or check out the output, navigate to TextPermutator/bin/Debug/ and you should find both input.txt and output.txt
---
More about this project
- This project was created in Visual Studio as a learning experience for me
---
Bugs!
- At a certain length of input (meaning if a word is too long), the program will take an incredibly long time to complete
    - Look into altering or just overhauling the current algorithm
    - Also look into better handling duplicate permutations with words containing repeated letters
- The program prints the vast majority, but still not all, of permutations even though the final list of permutations always contains the correct number of items
    - Look into checking the list's contents throughout the program's execution