# creates a class Html_document with methods that 1) find all links and 
# 2) pick internal links from this set and return the array of internal links presented as absolute


class Html
  def initialize(url, content)
    @url, @content = url, content
  end
  def find_all_links
    @all_links = @content.scan(/href\s?=.*?>/)
  end
  def pick_internal_links
    @relative_links, @absolute_links = [], []
    self.find_all_links.each do |link|
      case
        when link.start_with?("href=\"\/")
          @relative_links << @url + link.slice!(6...-3)
        when link.start_with?("href=\"#{@url}")
          @absolute_links << link.slice!(7...-3)
      end
    end
    (@relative_links + @absolute_links).uniq
  end
end

# example
# require_relative '1_30.rb'
# lenta = Webpage.new('lenta', 'http://www.lenta.ru')
# lenta_html = Html.new(lenta.page_url, lenta.download_html)
# aFile = File.new("links", "w+")
#    aFile << lenta_html.pick_internal_links
# aFile.close