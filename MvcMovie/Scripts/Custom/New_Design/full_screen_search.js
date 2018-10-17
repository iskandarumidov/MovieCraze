var tmdbSearchUrlBase = "https://api.themoviedb.org/3/search/movie?include_adult=false&page=1&language=en-US&api_key=e983ea919157d96035d850375451074a&query=";
var localSearchUrlBase = "http://localhost:1235/Movies/SearchMovies?";

var prevGenreDropDownVal = "---";
var prevGenreDropDownText = "---";
var placehold;
$( document ).ready(function() {
	$('select.ui').val(prevGenreDropDownVal);
	$('select.ui option:selected').text(prevGenreDropDownText);
	
	$("select.ui").change(function(){
		if($('#searchInStockOnlyChbx').is(':checked')){
		var query = localSearchUrlBase + "movieGenre=" + $('select.ui option:selected').val() + "&" + "searchString=" + encodeURIComponent($('#liveSearchBox').val());
		searchTmdb(query);
		}else{
			searchTmdb(tmdbSearchUrlBase + encodeURIComponent($('#liveSearchBox').val()));
		}
	});
	
	$('#searchInStockOnlyChbx').change(function() {
        if(this.checked) {
            var query = localSearchUrlBase + "movieGenre=" + $('select.ui option:selected').val() + "&" + "searchString=" + encodeURIComponent($('#liveSearchBox').val());
			searchTmdb(query);
            //$(this).prop("checked", returnVal);
			populateGenresDropbox(genresLocalJson);
        }else{
			searchTmdb(tmdbSearchUrlBase + encodeURIComponent($('#liveSearchBox').val()));
			populateGenresDropbox(genresTmdbJson);
		}
        //$('#textbox1').val(this.checked);        
    });
});



function openSearch() {
    document.getElementById("myOverlay").style.display = "block";
	$('#liveSearchBox').focus();
	$('#liveSearchBox').attr("placeholder", placehold);
	$('select.ui').val(prevGenreDropDownVal);
	$('select.ui option:selected').text(prevGenreDropDownText);
	populateGenresDropbox(genresTmdbJson);
}

function closeSearch() {
    document.getElementById("myOverlay").style.display = "none";
	placehold = $('#liveSearchBox').val();
	prevGenreDropDownVal = $('select.ui').val();
	prevGenreDropDownText = $('select.ui option:selected').text();
	$('#liveSearchBox').val('');
}


$(function() {
  var timer;
  $("#liveSearchBox").keyup(function() {
    clearTimeout(timer);
    var ms = 200; //Millisec to wait until sending request
    var val = this.value;
    timer = setTimeout(function() {
	  if($('#searchInStockOnlyChbx').is(':checked')){
		var query = localSearchUrlBase + "movieGenre=" + $('select.ui option:selected').val() + "&" + "searchString=" + encodeURIComponent($('#liveSearchBox').val());
		searchTmdb(query);
		//populateGenresDropbox(genresLocalJson);
	  }else{
		searchTmdb(tmdbSearchUrlBase + encodeURIComponent(val));
		prevGenreDropDownVal = $('select.ui').val();
		prevGenreDropDownText = $('select.ui option:selected').text();
		//populateGenresDropbox(genresTmdbJson);
	  }
    }, ms);
  });
});

function populateGenresDropbox(genresObjFromServer){
	var allGenresArr = genresObjFromServer.getJson().genres;
	//Clear all genre options
	$('select.ui').empty();
	if(!(prevGenreDropDownText == "---")){
		$('select.ui').append("<option value='---'>---</option>");
		$('select.ui').append("<option selected value='"+ prevGenreDropDownVal +"'>"+ prevGenreDropDownText +"</option>");
	}else{
		$('select.ui').append("<option selected value='---'>---</option>");
	}
	
	
	for(var k = 0; k < allGenresArr.length; k++){
		var currentGenre = allGenresArr[k].name;
		if(currentGenre == prevGenreDropDownText){
			continue;
		}
		
		$('select.ui').append("<option value='"+ allGenresArr[k].id +"'>"+ currentGenre+ "</option>");
	}
}


function generateSearchResultDivs(responseObj){
	var genresRequiredNum;
	var imageBaseUrl = "http://image.tmdb.org/t/p/w154";
	var searchResContainer = $("#searchResultsContainer");
	var resNumOrConstraint;
	var imageUrl;
	var rating;
	var voteCount;
	var overview;
	var genres;	
	
	searchResContainer.empty();
	//IMPLEMENT FOR LOCAL SEARCH!!
	if(!($('select.ui').val() == "---")){
		for(var i = responseObj.results.length-1; i >= 0 ; i--){
			var includes = 0;
			for(var j = 0; j < responseObj.results[i].genre_ids.length; j++){
				if(responseObj.results[i].genre_ids[j] == $('select.ui').val()){
					console.log("id: " + i + " "+ responseObj.results[i].title + " includes " + $('select.ui').val());
					includes = 1;
					break;
				}/* else{
					
				} */
				
			}
			if(!includes){
				console.log("id: " + i + " "+ responseObj.results[i].title + " does not include " + $('select.ui').val());
				responseObj.results.splice(i, 1);
			}
			
		}
		
		
		
		
	}
	console.log(responseObj.results);
	
	//Validate num of results on the page
	resNumOrConstraint = 4;
	if(responseObj.results.length < resNumOrConstraint){
		resNumOrConstraint = responseObj.results.length;
	}
	
	//Checking the variables
 	for(var i = 0; i < resNumOrConstraint; i++){
		var currentRespResults = responseObj.results[i];
		var title = currentRespResults.title;
		
		imageUrl = imageBaseUrl;
		if(String.isNullOrEmpty(currentRespResults.backdrop_path)){
			if(String.isNullOrEmpty(currentRespResults.poster_path)){
				imageUrl = "http://www.triumphowners.com/wp-content/plugins/triumph-cars/imgs/no-image-set.png";
			}else{
				imageUrl += currentRespResults.poster_path;
			}
		}else{
			imageUrl += currentRespResults.backdrop_path;
		}
		
		//Validate rating
		rating = 0;
		if(!(String.isNullOrEmpty(currentRespResults.vote_average))){
			rating = currentRespResults.vote_average;
		}
		
		voteCount = 0;
		if(!(String.isNullOrEmpty(currentRespResults.vote_count))){
			voteCount = currentRespResults.vote_count;
		}
		
		overview = "No description";
		if(currentRespResults.overview.length > 0){
			overview = currentRespResults.overview;
			if(overview.length > 147){
				overview = overview.substring(0, 147);
				overview += "...";
			}
		}
		
		
		
		//var imageUrl = imageBaseUrl + responseObj.results[i].backdrop_path;
		//Check if it's a year from a local database
		
		if(String(responseObj.results[i].release_date).length == 4){
			var date = new Date(responseObj.results[i].release_date, 1, 1);
			var year = "(" + date.getFullYear() + ")";
			//var date = new Date(parseInt(responseObj.results[i].release_date.substr(6)));
		}else{
			var date = new Date(responseObj.results[i].release_date);
			var year = "(" + date.getFullYear() + ")";
		}
		
		
		
		
		
		var appendString = '<div class="panel panel-default"><div class="panel-body"><div class="liveSearchContainer liveSearchThumbnailContainer"><img class="liveSearchThumbnail" src="' + imageUrl + '"></div><div class="liveSearchParamContainer"><div class="liveSearchName"><b>' + title + " " + year + '</b></div><div class="liveSearchGenres">Drama, Action, Sci-Fi, Fighting...</div><div class="liveSearchPopularity"><div class="star-ratings-sprite"><span style="width:33%" class="star-ratings-sprite-rating"></span></div>('+ voteCount +')</div></div><div class="liveSearchDescription">'+ overview +'</div><div class="liveSearchActionLinks"><div class="liveSearchIsInStock">In stock</div><div class="liveSearchRequestMovie">Request</div><div class="liveSearchAddToCart">Add to Cart</div></div></div></div>';
		$("#searchResultsContainer").append(appendString);
		
		
		genres = "No genres";
		genresRequiredNum = 4;
		if(currentRespResults.genre_ids.length > 0){
			if(currentRespResults.genre_ids.length < 4){
				genresRequiredNum = currentRespResults.genre_ids.length;
			}
			genres = "";
			var allGenresArr = genresTmdbJson.getJson().genres;
			console.log(allGenresArr);
			for(var j = 0; j < genresRequiredNum; j++){
				var currentId = currentRespResults.genre_ids[j];
				for(var k = 0; k < allGenresArr.length; k++){
					if(allGenresArr[k.toString()].id == currentId){
						var currentGenre = allGenresArr[k.toString()].name;
						genres += currentGenre + ",";
					}
				}
			}
			if(genres.charAt(genres.length-1) == ","){
				genres = genres.substring(0, genres.length - 1);
			}
		}
		
		
		
		
		$(".liveSearchGenres").eq(i).text(genres);
		//Multiply rating by 10 to get percentage. Apply to stars' width
		$(".star-ratings-sprite-rating").eq(i).css("width", rating * 10 + "%");
	}	 
	$("#searchResultsContainer .panel").first().addClass("firstPanel");
}

//Searches the local DB, not TMDB
function searchLocalDB(query){
	alert("local search");
}

function searchTmdb(query) {
  $.ajax({
     type: "GET",
     url: query,
     dataType: "json",
     success: function(json) {
       console.log("sent");
	   generateSearchResultDivs(json);
     }
  });
}

//Genres from the TMDB server; used if in-stock only is unchecked
var genresTmdbJson = (function(){
    var json;

    $.ajax({
      type: "GET",
      url: "https://api.themoviedb.org/3/genre/movie/list?api_key=e983ea919157d96035d850375451074a&language=en-US",
      dataType: "json",
	  async: false,
      success : function(data) {
                    json = data;
                }
    });

    return {getJson : function()
    {
        if (json) return json;
        // else show some error that it isn't loaded yet;
    }};
})();

var genresLocalJson = (function(){
    var json;

    $.ajax({
      type: "GET",
      url: "http://localhost:1235/Movies/GetAvailableGenres",
      dataType: "json",
	  async: false,
      success : function(data) {
                    json = data;
                }
    });

    return {getJson : function()
    {
        if (json) return json;
        // else show some error that it isn't loaded yet;
    }};
})();



//Only genres contained in the local database; used if in-stock only is checked
/* var genresLocalDB = (function(){
    var json;

    $.ajax({
      type: "GET",
      url: "https://api.themoviedb.org/3/genre/movie/list?api_key=e983ea919157d96035d850375451074a&language=en-US",
      dataType: "json",
	  async: false,
      success : function(data) {
                    json = data;
                }
    });

    return {getJson : function()
    {
        if (json) return json;
        // else show some error that it isn't loaded yet;
    }};
})(); */

//Helper function
String.isNullOrEmpty = function (value) {
    return !value;
}