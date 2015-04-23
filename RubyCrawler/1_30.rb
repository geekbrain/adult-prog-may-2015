# creates a class called Webpage with a method download_html that downloads that page's html as a string

require 'open-uri'

class Webpage
	attr_accessor :page_name, :page_url
	def initialize(name, url)
		@page_name, @page_url = name, url
	end
	def download_html
  		open(@page_url).read
  	end
end

# example:
# lenta = Webpage.new('lenta', 'http://www.lenta.ru')
# page_content = lenta.download_html