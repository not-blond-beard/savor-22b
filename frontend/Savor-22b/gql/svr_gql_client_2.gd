extends GQLClient

func _ready():
	print("client 2 ready")
	#set_endpoint(false, "1.230.253.103", 38080, "/graphql")
	
	set_endpoint(false, "localhost", 38080, "/graphql/explorer")
	

func raw_mutation(query:String):
	return GQLQueryExecuter.new(endpoint, use_ssl, Mutation.new(query))
