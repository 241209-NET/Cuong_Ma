/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from "react";
import axios from "axios";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";

const baseURL = "http://localhost:5108";

const App = () => {
  const [tweets, setTweets] = useState<any[]>([]);
  const [newTweet, setNewTweet] = useState("");
  const [likedTweets, setLikedTweets] = useState<number[]>([]); 
  const [replyBody, setReplyBody] = useState("");
  const [replyParentId, setReplyParentId] = useState<number | null>(null); 

    const userId = 5; 

  // fetch tweets
  const fetchTweets = async () => {
    try {
      const response = await axios.get(`${baseURL}/api/Tweet`);
      setTweets(response.data);
    } catch (error) {
      console.error("Error fetching tweets:", error);
    }
  };


  // post a new tweet
  const handlePostTweet = async () => {
    if (!newTweet.trim()) return;

    const payload = {
      body: newTweet,
      userId,
    };

    try {
      const response = await axios.post(`${baseURL}/api/Tweet`, payload);
      setTweets([response.data, ...tweets]);
      setNewTweet("");
    } catch (error) {
      console.error("Error posting tweet:", error);
    }
  };

  // post a reply
  const handlePostReply = async (parentId: number) => {
    if (!replyBody.trim()) return;

    const payload = {
      body: replyBody,
      userId,
      parentId,
    };

    try {
      const response = await axios.post(`${baseURL}/api/Tweet`, payload);
      setTweets([response.data, ...tweets]);
      setReplyBody("");
      setReplyParentId(null); 
    } catch (error) {
      console.error("Error posting reply:", error);
    }
  };

  // like or unlike a tweet
  const handleToggleLike = async (id: number) => {
    const isLiked = likedTweets.includes(id);

    try {
      if (isLiked) {
        // unlike the tweet
        await axios.post(`${baseURL}/api/Tweet/${id}/Unlike`);
        setLikedTweets(likedTweets.filter((tweetId) => tweetId !== id));
        setTweets(
          tweets.map((tweet) =>
            tweet.id === id ? { ...tweet, likes: tweet.likes - 1 } : tweet
          )
        );
      } else {
        // like the tweet
        await axios.post(`${baseURL}/api/Tweet/${id}/Like`);
        setLikedTweets([...likedTweets, id]);
        setTweets(
          tweets.map((tweet) =>
            tweet.id === id ? { ...tweet, likes: tweet.likes + 1 } : tweet
          )
        );
      }
    } catch (error) {
      console.error("Error toggling like:", error);
    }
  };

  // delete a tweet
  const handleDeleteTweet = async (id: number) => {
    try {
      await axios.delete(`${baseURL}/api/Tweet/${id}`);
      setTweets(tweets.filter((tweet) => tweet.id !== id)); 
    } catch (error) {
      console.error("Error deleting tweet:", error);
    }
  };

  useEffect(() => {
    fetchTweets();
  }, []);

  const renderTweet = (tweet: any) => (
    <Card key={tweet.id} className="shadow-sm bg-slate-800 text-white border-slate-600">
      <CardHeader>
        <CardTitle>{tweet.user.username || "Anonymous"}</CardTitle>
      </CardHeader>
      <CardContent>
        <p className="text-sm text-white">{tweet.body}</p>
        <p className="text-xs text-slate-300">{new Date(tweet.createdAt).toLocaleString()}</p>
      </CardContent>
      <CardFooter className="flex justify-start space-x-2">
        <Button
          variant={likedTweets.includes(tweet.id) ? "destructive" : "default"}
          onClick={() => handleToggleLike(tweet.id)}
        >
          {likedTweets.includes(tweet.id) ? "Unlike" : "Like"} ({tweet.likes})
        </Button>
        <Button onClick={() => setReplyParentId(tweet.id)}>Reply</Button>
        <Button variant="destructive" onClick={() => handleDeleteTweet(tweet.id)}>
          Delete
        </Button>
      </CardFooter>
      {replyParentId === tweet.id && (
        <div className="mt-2 p-4">
          <Textarea
            className="border-slate-600 placeholder:text-slate-300"
            placeholder="Write your reply..."
            value={replyBody}
            onChange={(e) => setReplyBody(e.target.value)}
          />
          <Button
            onClick={() => handlePostReply(tweet.id)}
            className="mt-2"
          >
            Post Reply
          </Button>
        </div>
      )}
      {/* render replies if avail */}
      <div className="ml-6 mt-4 space-y-4">
        {tweets
          .filter((reply) => reply.parentId === tweet.id)
          .map((reply) => renderTweet(reply))}
      </div>
    </Card>
  );

  return (
    <div className="min-h-screen bg-slate-800 text-white">
      {/* top navbar */}
      <div className="bg-slate-600 border-b border-slate-700">
        <div className="max-w-2xl flex justify-start items-center py-4 px-4">
          <h1 className="text-xl font-bold text-white">Cuong's Social Media APP</h1>
        </div>
      </div>

      <div className="max-w-2xl mx-auto mt-6 px-4">
        {/* tweet box */}
        <Card className="mb-6 bg-slate-800 border-slate-600">
          <CardContent>
            <Textarea
              className="border-slate-600 placeholder:text-slate-300 mt-5"
              placeholder="What's happening?"
              value={newTweet}
              onChange={(e) => setNewTweet(e.target.value)}
            />
          </CardContent>
          <CardFooter>
            <Button onClick={handlePostTweet}>Post Tweet</Button>
          </CardFooter>
        </Card>

        {/* tweet feed */}
        <div className="space-y-4">
          {tweets.filter((tweet) => !tweet.parentId).map((tweet) => renderTweet(tweet))}
        </div>
      </div>
    </div>
  );
};

export default App;
