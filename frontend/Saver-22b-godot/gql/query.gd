var get_villages_query = "query {
	villages {
		id
		name
		width
		height
		worldX
		worldY
	}
}"

var place_house_query_format = "query {
  createAction_PlaceUserHouse(
	publicKey: {},
  	villageId: {},
	x: {},
	y: {}
  )
}"
