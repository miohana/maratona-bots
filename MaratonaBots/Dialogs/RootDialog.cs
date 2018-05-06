using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MaratonaBots.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // "**" deixa em negrito!
            await context.PostAsync("**Olá, tudo bem?**");

            // estrutura para criação de conteúdo
            var message = activity.CreateReply();

            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var heroCard = new HeroCard
                {
                    Title = "Planeta",
                    Subtitle = "Universo",
                    Images = new List<CardImage>
                    {
                        new CardImage("https://abrilexame.files.wordpress.com/2017/11/planetaterra.jpg", "Planeta", new CardAction(ActionTypes.OpenUrl, "Microsoft", value: "www.microsoft.com"))
                    },
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Text = "Botão 1",
                            DisplayText = "Display",
                            Title = "Title",
                            Type = ActionTypes.PostBack,
                            Value = "aqui vai um valor"
                        }
                    }
                };

                message.Attachments.Add(heroCard.ToAttachment());
            }
            else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var videoCard = new VideoCard
                {
                    Title = "Um vídeo qualquer",
                    Subtitle = "Aqui vai um subtítulo",
                    Autostart = true,
                    Autoloop = false,
                    Media = new List<MediaUrl>
                    {
                        new MediaUrl("http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4")
                    }
                };

                message.Attachments.Add(videoCard.ToAttachment());
            }
            else if (activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAnimationCard();

                message.Attachments.Add(attachment);
            }
            else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAnimationCard();

                message.Attachments.Add(attachment);
            }
            else if (activity.Text.Equals("carousel", StringComparison.InvariantCultureIgnoreCase))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var audio = CreateAudioCard();
                var animation = CreateAnimationCard();

                message.Attachments.Add(audio);
                message.Attachments.Add(animation);
            }

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimationCard()
        {
            var animationCard = new AnimationCard
            {
                Title = "Um áudio revelador",
                Subtitle = "Aqui vai um subtítulo",
                Autostart = true,
                Autoloop = false,
                Media = new List<MediaUrl>
                    {
                        new MediaUrl("https://img.buzzfeed.com/buzzfeed-static/static/enhanced/webdr06/2013/5/31/10/anigif_enhanced-buzz-3734-1370010471-16.gif")
                    }
            };

            return animationCard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audioCard = new AudioCard
            {
                Title = "Um áudio revelador",
                Subtitle = "Aqui vai um subtítulo",
                Autostart = true,
                Autoloop = false,
                Image = new ThumbnailUrl("https://abrilexame.files.wordpress.com/2017/11/planetaterra.jpg", "Planeta"),
                Media = new List<MediaUrl>
                    {
                        new MediaUrl("http://www.wavlist.com/movies/004/father.wav")
                    }
            };

            return audioCard.ToAttachment();
        }
    }
}