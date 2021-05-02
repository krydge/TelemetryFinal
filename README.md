# 
![Logo](Logo.png)

A Personal Assistant Engine built with .NET Core with Natural Language Processing.
The NLP capabilities are powered by the **Starlight Core** (https://github.com/MeiFagundes/Starlight).



Example:

**Query:** *Wake me up at 10:30 AM* 

**Current date/time:** 10-nov-2019, 12:30 PM

**Output:**

```
{
  "code": 41,
  "response": "Sure, I've set an alarm for tomorrow, 10:30 AM.",
  "entities": {
    "entity": "tomorrow",
    "type": "date",
    "startIndex": 14,
    "endIndex": 18,
    "date": "2019-11-11",
    "time": "10:30 AM"
  }
}
```
## Instructions to get PolarisAICore running
Pull the repo https://github.com/krydge/TelemetryFinal
Run this docker command
Change the volume location to one on your own device. C:\Users\krydg\Desktop\TelelmetryFinal:/data
This is where I decided to store my volume. You must make it a good location on your own computer.
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -v C:\Users\krydg\Desktop\TelelmetryFinal:/data -p 8081:80 datalust/seq:latest
Once the Seq Container is running go to the polarisAI project. Set PolarisAICore as the startup project. Then run the project like any other project you are working on.
(Optional) If you want to get elasticsearch running 
Run this Docker command
docker run -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:7.12.1
Then to run kibana run
docker run --link YOUR_ELASTICSEARCH_CONTAINER_NAME_OR_ID:elasticsearch -p 5601:5601 docker.elastic.co/kibana/kibana:7.12.1
To find your container name or id use docker ps.
You will then need to point your elasticsearch data to the kibana
