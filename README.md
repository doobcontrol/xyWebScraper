# xyWebScraper
This project was inspired by me learning Python while practicing writing a web image scraper, but there were some features I couldn't implement using Python, so I turned to C# Winforms.  
My English writing are not that good. Therefore, I would welcome any comments on my writing in addition to comments on my project itself.
## Features
1, you can define any web page model to find the files you want to scrape in that type of web page.  
2, there are three type of definitions, file save path definition, files search definition and url search definition.  
## Instructions
User must define the page model of web pages he want to scraping first, then select the page model defined, input/paste the actual Url, and click start.  
Each page model can contain three optional category definitions, namely file save path definition, file search definition and URL search definition. Each category can include multiple actual definitions. Every actual definitions defines how to search specific content that category need from HTML content get from web site.  
### file save path definition
Define how to search information to create the path to save the file scrape from the same web page. You can make multiple definition to create multiple layer path.
### files search definition
Define how to search actual file urls you want to scrape in that web page.
### url search definition
Define how to search urlls that navgate to other web page after the files found in the web page have downloaded.  
