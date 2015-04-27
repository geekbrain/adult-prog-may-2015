# class "News_text" has contents string as attribute and a method called "compare" that takes a list of search items as regular expressions 
# and returns a hash where search items are mapped onto the number of their occurences in the contents string

class News_text
	def initialize(contents)
		@contents = contents
	end
	def quantity_of_occurences(word)
		@contents.scan(word).length
	end
	def compare(list_of_search_items)
		@comparison = Hash.new
		list_of_search_items.each do |search_item|
			@comparison[search_item] = self.quantity_of_occurences(search_item)
		end
		@comparison
	end
end

# ## example:
# search_item1 = /Медведев.*?/
# search_item2 = /Навальн.*/
# search_item3 = /(Путин.*?)|((п|П)резидент.{0,2}\s(РФ|России|Российской\sФедерации))/
# a = News_text.new("блабла Медведев Путиным погоняет Навальный Навальному не товарищ, а Президент Российской Федерации")
# p a.compare([search_item1, search_item2, search_item3])