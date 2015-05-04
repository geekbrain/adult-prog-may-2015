# class "News_text" has contents string as attribute and a method called "compare" that takes a list of search items as regular expressions 
# and returns a hash where search items are mapped onto the number of their occurences in the contents string

class News_text
	def initialize(contents)
		@contents = contents
	end
	def quantity_of_occurences(word)
		@contents.scan(word).length
	end
	def compare(hash_of_search_items)
		@comparison = Hash.new
		hash_of_search_items.each do |search_item, synonyms|
			@comparison[search_item] = 0
			synonyms.each do |synonym|
				@comparison[search_item] += self.quantity_of_occurences(synonym)
			end
		end
		@comparison
	end
end

# #example:
# evil_guys = {'Медведев' => ['Медведев'], 'Навальный' => ['Навальный', 'Навальному'], 'Путин' => ['Путин', 'президент РФ']}
# a = News_text.new("блабла Медведев Путиным погоняет Навальный Навальному не товарищ, а Президент Российской Федерации")
# p a.compare(evil_guys)
