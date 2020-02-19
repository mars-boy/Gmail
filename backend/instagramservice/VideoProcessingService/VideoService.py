import json
import pika


credentials = pika.PlainCredentials('picsfeed', 'picsfeed')
connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost', credentials=credentials))
channel = connection.channel()
channel.exchange_declare(exchange='progressive-video-parser-exchange', exchange_type='direct', durable=True)
queue_name = 'progressive-video-parser-queue'
channel.queue_bind(exchange='progressive-video-parser-exchange', 
queue=queue_name,
 routing_key='create')

print(' [*] Waiting for video paths. To exit press CTRL+C')

def callback(ch, method, properties, body):
    savepath = json.loads(body)['SavePath']
    print(savepath)
    ch.basic_ack(method.delivery_tag)


channel.basic_consume(
    queue=queue_name, on_message_callback=callback, auto_ack=False)

channel.start_consuming()