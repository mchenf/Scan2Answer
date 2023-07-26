using System.Runtime.CompilerServices;

namespace Scan2Answer.Models
{
    public class Question
    {
        public string Statement { get; set; }
        public QuestionOption[] Options { get; set; }
        public int optionLength() => Options.Length;
    }

    public struct QuestionOption
    {
        public QuestionOption(bool isCorrect, string option)
        {
            Option = option;
            IsCorrect = isCorrect;
        }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class MockQuestionSet
    {
        private Guid id { get; set; }
        public string SessionId { get => id.ToString(); }
        public MockQuestionSet()
        {
            questions = new Question[]
            {
                new Question
                {
                    Statement = "请选择您的回答",
                    Options = new QuestionOption[]
                    {
                        new QuestionOption(false, "A"),
                        new QuestionOption(false, "B"),
                        new QuestionOption(false, "C"),
                        new QuestionOption(false, "D")
                    }
                }

            };

            id = Guid.NewGuid();
        }
        private Question[] questions { get; set; }
        public Question this[int index]
        {
            get => questions[index];
        }

    }
}
