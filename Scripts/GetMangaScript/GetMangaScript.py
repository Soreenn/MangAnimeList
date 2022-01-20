import requests
import json
import time
id = 30001
# Here we define our query as a multi-line string
query = '''
query ($id: Int) { # Define which variables will be used in the query (id)
  Media (id: $id, type: MANGA) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
        id
    title{
      english
      romaji
      native
    }
    averageScore
    volumes
    chapters
    status
    startDate{
      year
      month
      day
    }
    tags{
      name
    }
    coverImage{
      extraLarge
      medium
    }
    bannerImage
  }
}
'''
# Define our query variables and values that will be used in the query request
variables = {
    'id': 30001
}
url = 'https://graphql.anilist.co'
# Make the HTTP Api request
while id < 40000:
  variables['id'] = id
  response = requests.post(url, json={'query': query, 'variables': variables}).json
  datas = response()
  virgule = ','
  if not "errors" in datas:
    print(id)
    with open('list.json', 'a') as f:
      json.dump(response(), f)
      json.dump(virgule, f)
    time.sleep(0.5)
  id += 1
  
